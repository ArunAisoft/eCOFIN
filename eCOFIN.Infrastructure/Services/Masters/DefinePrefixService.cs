using System.Data;
using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace eCOFIN.Infrastructure.Services.Masters
{
    /// <summary>
    /// Define Prefix (cfn_accountottolink).
    ///
    /// Codes verified against the live table:
    ///     vchr_type  prod_prefixtype  rows
    ///     I          D                1711    Sales / Domestic
    ///     I          E                  53    Sales / Export
    ///     P          D                  62    Purchase / Domestic
    ///     P          I                   5    Purchase / Import
    /// So 'P' = Purchase and 'I' = Sales, and the prefix type codes differ
    /// per mode (Purchase D/I, Sales D/E).
    ///
    /// Design notes:
    ///
    /// 1. CfnAccountottolink is mapped [Keyless]. EF Core cannot track keyless
    ///    entities, so DbSet.Add / RemoveRange throw at runtime. All writes go
    ///    through parameterised raw SQL.
    ///
    /// 2. Target server is SQL Server 2008 Express. OFFSET/FETCH is 2012+, so
    ///    paging uses ROW_NUMBER(). Parameters are declared VarChar to match
    ///    the char(n) columns; NVarChar would add CONVERT_IMPLICIT and defeat
    ///    the indexes.
    ///
    /// 3. Account and customer lookups are cached in-process for 10 minutes.
    /// </summary>
    public class DefinePrefixService : IDefinePrefixService
    {
        private readonly BilzFinDbContext _context;
        private readonly IMemoryCache _cache;

        /// <summary>cfn_referencectrl.referencetype. Verified present: 4493/5467.</summary>
        private const string ReferenceType = "VENDR";

        /// <summary>Legacy window name, used to resolve the task id (438).</summary>
        private const string InterfaceRef = "w_accountottolink";

        private const string HelpTopicCustomer = "customercode";
        private const string HelpTopicAccount = "accountcode";

        private const string CacheKeyAccounts = "prefix:accounts";
        private const string CacheKeyCustomers = "prefix:customers";
        private static readonly TimeSpan LookupTtl = TimeSpan.FromMinutes(10);

        // Actual column widths in cfn_accountottolink.
        private const int PrefixLen = 15;
        private const int AccountLen = 10;

        public DefinePrefixService(BilzFinDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        /* ==================== READ ==================== */

        public async Task<PagedResult<PrefixLinkDto>> GetLinksAsync(
            string vchrType,
            string prefixType,
            string? search = null,
            int page = 1,
            int pageSize = 50)
        {
            var result = new PagedResult<PrefixLinkDto>
            {
                Page = page < 1 ? 1 : page,
                // Add mode loads the whole scope in one page, because SaveLinks
                // replaces every row for (vchrType, prefixType). The largest
                // scope on this database is Sales/Domestic at 1711 rows, so the
                // old cap of 500 silently reset a full load back to 50.
                PageSize = pageSize < 1 ? 50 : (pageSize > 5000 ? 5000 : pageSize)
            };

            if (string.IsNullOrWhiteSpace(vchrType) || string.IsNullOrWhiteSpace(prefixType))
                return result;

            var term = string.IsNullOrWhiteSpace(search) ? null : search.Trim();
            var skip = (result.Page - 1) * result.PageSize;

            const string whereClause = @"
                    l.vchr_type       = @vchr
                AND l.prod_prefixtype = @ptype
                AND (l.objectstatus IS NULL OR l.objectstatus <> 'OBSLT')
                AND a.accountstatus <> 'OBSLT'
                AND (@term IS NULL
                     OR l.prod_prefix  LIKE @like
                     OR l.accountcode  LIKE @like
                     OR a.description  LIKE @like
                     OR c.customername LIKE @like)";

            var countSql = $@"
                SELECT COUNT_BIG(1)
                FROM   dbo.cfn_accountottolink l
                JOIN   dbo.cfn_account  a ON a.accountcode  = l.accountcode
                LEFT JOIN dbo.cfn_customer c ON c.customercode = l.prod_prefix
                WHERE {whereClause};";

            // ROW_NUMBER paging - SQL Server 2008 compatible.
            var pageSql = $@"
                SELECT rn.prod_prefix     AS ProdPrefix,
                       rn.accountcode     AS AccountCode,
                       rn.description     AS Description,
                       rn.vchr_type       AS VchrType,
                       rn.prod_prefixtype AS ProdPrefixType,
                       rn.prod_levytype   AS ProdLevyType,
                       rn.dbcrflag        AS DbcrFlag,
                       rn.customername    AS CustomerName,
                       rn.objectstatus    AS ObjectStatus
                FROM (
                    SELECT ROW_NUMBER() OVER (ORDER BY l.prod_prefix, l.accountcode) AS seq,
                           RTRIM(l.prod_prefix)     AS prod_prefix,
                           RTRIM(l.accountcode)     AS accountcode,
                           RTRIM(a.description)     AS description,
                           RTRIM(l.vchr_type)       AS vchr_type,
                           RTRIM(l.prod_prefixtype) AS prod_prefixtype,
                           RTRIM(l.prod_levytype)   AS prod_levytype,
                           RTRIM(l.dbcrflag)        AS dbcrflag,
                           RTRIM(c.customername)    AS customername,
                           RTRIM(l.objectstatus)    AS objectstatus
                    FROM   dbo.cfn_accountottolink l
                    JOIN   dbo.cfn_account  a ON a.accountcode  = l.accountcode
                    LEFT JOIN dbo.cfn_customer c ON c.customercode = l.prod_prefix
                    WHERE {whereClause}
                ) rn
                WHERE rn.seq > @skip AND rn.seq <= @skip + @take
                ORDER BY rn.seq;";

            var conn = _context.Database.GetDbConnection();
            var opened = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    await _context.Database.OpenConnectionAsync();
                    opened = true;
                }

                await using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = countSql;
                    cmd.CommandTimeout = 60;
                    AddScopeParams(cmd, vchrType, prefixType, term);
                    var scalar = await cmd.ExecuteScalarAsync();
                    result.TotalCount = scalar is null or DBNull ? 0 : Convert.ToInt32(scalar);
                }

                if (result.TotalCount == 0) return result;

                await using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = pageSql;
                    cmd.CommandTimeout = 60;
                    AddScopeParams(cmd, vchrType, prefixType, term);
                    cmd.Parameters.Add(new SqlParameter("@skip", SqlDbType.Int) { Value = skip });
                    cmd.Parameters.Add(new SqlParameter("@take", SqlDbType.Int) { Value = result.PageSize });

                    await using var rdr = await cmd.ExecuteReaderAsync();
                    while (await rdr.ReadAsync())
                    {
                        result.Items.Add(new PrefixLinkDto
                        {
                            ProdPrefix = Str(rdr, 0),
                            AccountCode = Str(rdr, 1),
                            Description = Str(rdr, 2),
                            VchrType = Str(rdr, 3),
                            ProdPrefixType = Str(rdr, 4),
                            ProdLevyType = Str(rdr, 5),
                            DbcrFlag = Str(rdr, 6),
                            CustomerName = Str(rdr, 7),
                            ObjectStatus = rdr.IsDBNull(8) ? null : rdr.GetString(8)
                        });
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving prefix links: " + ex.Message, ex);
            }
            finally
            {
                if (opened) await _context.Database.CloseConnectionAsync();
            }
        }

        /// <summary>Both lookup lists in a single call, served from cache.</summary>
        public async Task<PrefixLookupsDto> GetLookupsAsync()
        {
            // Await each call directly. Assigning the Task to a variable first
            // STARTS it, which puts two concurrent queries on one DbContext and
            // throws "A second operation was started on this context instance".
            var accounts = await GetAccountsAsync();
            var customers = await GetCustomersAsync();

            return new PrefixLookupsDto
            {
                Accounts = accounts,
                Customers = customers
            };
        }

        public async Task<List<AccountLookupDto>> GetAccountsAsync()
        {
            if (_cache.TryGetValue(CacheKeyAccounts, out List<AccountLookupDto>? cached) && cached is not null)
                return cached;

            try
            {
                var result = await _context.CfnAccounts
                    .AsNoTracking()
                    .Where(x => x.Accountstatus != "OBSLT" && x.CtrlNextrefrflag == "N")
                    .OrderBy(x => x.Accountcode)
                    .Select(x => new AccountLookupDto
                    {
                        AccountCode = x.Accountcode,
                        Description = x.Description ?? ""
                    })
                    .ToListAsync();

                // char(n) columns come back blank-padded; trim once here so the
                // browser is not comparing "AC001     " against "AC001".
                foreach (var a in result)
                {
                    a.AccountCode = a.AccountCode.TrimEnd();
                    a.Description = a.Description.TrimEnd();
                }

                _cache.Set(CacheKeyAccounts, result, LookupTtl);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving accounts: " + ex.Message, ex);
            }
        }

        public async Task<List<CustomerLookupDto>> GetCustomersAsync()
        {
            if (_cache.TryGetValue(CacheKeyCustomers, out List<CustomerLookupDto>? cached) && cached is not null)
                return cached;

            try
            {
                var result = await _context.CfnCustomers
                    .AsNoTracking()
                    .Where(x => x.Objectstatus == null || x.Objectstatus != "OBSLT")
                    .OrderBy(x => x.Customercode)
                    .Select(x => new CustomerLookupDto
                    {
                        CustomerCode = x.Customercode,
                        CustomerName = x.Customername ?? ""
                    })
                    .ToListAsync();

                foreach (var c in result)
                {
                    c.CustomerCode = c.CustomerCode.TrimEnd();
                    c.CustomerName = c.CustomerName.TrimEnd();
                }

                _cache.Set(CacheKeyCustomers, result, LookupTtl);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving customers: " + ex.Message, ex);
            }
        }

        /* ==================== ADD CONTEXT ==================== */

        /// <summary>
        /// Everything the Add button needs, in one round trip.
        ///
        /// Mirrors the legacy Add sequence from the profiler trace:
        ///   1. select taskid from cfn_task where taskinterfacerefr = 'w_accountottolink'
        ///   2. SELECT levelnumber FROM cfn_userpermission WHERE username = ? AND taskid = ?
        ///   3. SELECT ... FROM cfn_genhelp WHERE helptopic = 'customercode' / 'accountcode'
        ///   4. select count(*) from cfn_voucheraccounts where vouchertype = 'VENDR'
        ///   5. SELECT ... FROM cfn_customer / cfn_account
        ///
        /// The legacy app repeated 3 and 5 three times per column, roughly
        /// twelve round trips. That chattiness is deliberately not reproduced.
        /// </summary>
        public async Task<PrefixAddContextDto> GetAddContextAsync(string username)
        {
            try
            {
                var user = (username ?? "").Trim();
                var ctx = new PrefixAddContextDto();

                // 1. Resolve the task id from the legacy window name.
                ctx.TaskId = await _context.CfnTasks.AsNoTracking()
                    .Where(t => t.Taskinterfacerefr == InterfaceRef)
                    .Select(t => t.Taskid)
                    .FirstOrDefaultAsync() ?? string.Empty;

                // 2. Permission level. Case-insensitive, matching the legacy
                //    WHERE Upper(username) = Upper('REKHA').
                if (!string.IsNullOrEmpty(ctx.TaskId) && user.Length > 0)
                {
                    var lvl = await _context.CfnUserpermissions.AsNoTracking()
                        .Where(p => p.Taskid == ctx.TaskId
                                 && p.Username.ToUpper() == user.ToUpper())
                        .Select(p => p.Levelnumber)
                        .FirstOrDefaultAsync();

                    ctx.LevelNumber = int.TryParse((lvl ?? "").Trim(), out var n) ? n : -1;
                }

                ApplyPermissions(ctx);

                // No permission at all: stop before shipping the customer master.
                if (!ctx.CanBrowse) return ctx;

                // 3. cfn_genhelp metadata for the editable columns.
                // Explicit OR, not HelpTopicKeys.Contains(...). EF Core 8+
                // translates Contains on an array into OPENJSON, which does
                // not exist in SQL Server 2008 and throws at runtime.
                var help = await _context.CfnGenhelps.AsNoTracking()
                    .Where(h => h.Helptopic == HelpTopicCustomer
                             || h.Helptopic == HelpTopicAccount)
                    .ToListAsync();

                foreach (var h in help)
                {
                    var key = (h.Helptopic ?? "").Trim().ToLowerInvariant();
                    if (key.Length == 0 || ctx.HelpTopics.ContainsKey(key)) continue;

                    ctx.HelpTopics[key] = new HelpTopicDto
                    {
                        HelpId = (h.Helpid ?? "").Trim(),
                        HelpTopic = key,
                        RegFlag = (h.Regflag ?? "").Trim(),
                        HelpObjectName = h.Helpobjectname?.Trim(),
                        HelpUpdateableColumn = h.Helpupdateablecolumn?.Trim(),
                        TableName = h.Tablename?.Trim(),
                        DestColumn = h.Destcolumn?.Trim()
                    };
                }

                // 4. Account restriction. Verified empty for VENDR on this
                //    database, so normally false - same as the legacy app.
                var restrictedCodes = await _context.CfnVoucheraccounts.AsNoTracking()
                    .Where(v => v.Vouchertype == ReferenceType)
                    .Select(v => v.Accountcode)
                    .ToListAsync();

                ctx.AccountsRestricted = restrictedCodes.Count > 0;

                // 5. Lookup lists (cached).
                var accounts = await GetAccountsAsync();
                var customers = await GetCustomersAsync();

                if (ctx.AccountsRestricted)
                {
                    var allow = new HashSet<string>(
                        restrictedCodes.Select(c => (c ?? "").Trim()),
                        StringComparer.OrdinalIgnoreCase);

                    accounts = accounts.Where(a => allow.Contains(a.AccountCode)).ToList();
                }

                ctx.Accounts = accounts;
                ctx.Customers = customers;

                return ctx;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error building add context: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Maps cfn_userpermission.levelnumber onto the legacy toolbar buttons.
        /// Levels in use on this database: 0 (6 users), 2 (3 users), 3 (8 users).
        /// Level 1 is unused, so in practice anyone who can Add can also Post.
        ///
        /// Adjust here if the business uses the levels differently.
        /// </summary>
        private static void ApplyPermissions(PrefixAddContextDto ctx)
        {
            var l = ctx.LevelNumber;

            ctx.LevelLabel = l switch
            {
                0 => "View",
                1 => "User",
                2 => "Super user",
                3 => "Supervisor",
                _ => "None"
            };

            ctx.CanBrowse = l >= 0;
            ctx.CanAdd = l >= 1;
            ctx.CanHold = l >= 1;
            ctx.CanPost = l >= 2;
            ctx.CanObsolete = l >= 3;
        }

        /* ==================== WRITE ==================== */

        /// <summary>
        /// Inserts the supplied rows. Existing rows for the scope are left
        /// alone - this is an append, not a replace. ctrlStatus is "Post" or
        /// "Hold", matching the legacy toolbar. "ONHOLD" from older callers
        /// is accepted and normalised to "Hold".
        /// Uses raw SQL because the entity is keyless.
        /// </summary>
        public async Task<(bool Success, string Message)> SaveLinksAsync(
            SavePrefixLinksRequest model,
            string ctrlStatus)
        {
            // Status values verified against the live database:
            //   cfn_gldetail.ctrl_status      -> 'Post' (2,772,123) / 'Hold' (1,104)
            //   cfn_accountottolink.ctrl_status -> 'Post' (1,658) / blank (174)
            // 'ONHOLD' appears nowhere, so the legacy convention is 'Hold'.
            var status = (ctrlStatus ?? "Post").Trim();
            if (string.Equals(status, "ONHOLD", StringComparison.OrdinalIgnoreCase))
                status = "Hold";
            if (status != "Post" && status != "Hold")
                return (false, "Status must be 'Post' or 'Hold'.");

            if (string.IsNullOrWhiteSpace(model.VchrType)) return (false, "Voucher Type is required.");
            if (string.IsNullOrWhiteSpace(model.PrefixType)) return (false, "Prefix Type is required.");
            if (model.Rows is null || model.Rows.Count == 0) return (false, "At least one row is required.");

            // Re-check permission server-side. A disabled button in the browser
            // is a convenience, not a control.
            var ctx = await GetAddContextAsync(model.Username ?? "");
            if (status == "Post" && !ctx.CanPost)
                return (false, $"Your access level ({ctx.LevelLabel}) does not permit Post.");
            if (status == "Hold" && !ctx.CanHold)
                return (false, $"Your access level ({ctx.LevelLabel}) does not permit On Hold.");

            var vchr = model.VchrType.Trim();
            var ptype = model.PrefixType.Trim();
            var user = Trunc(model.Username, 30) ?? "SYSTEM";
            var loc = Trunc(string.IsNullOrWhiteSpace(model.Location) ? "BILZ" : model.Location, 5)!;
            var now = DateTime.Now;

            // Validate before opening a transaction.
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in model.Rows)
            {
                if (string.IsNullOrWhiteSpace(r.ProdPrefix)) return (false, "Customer/Levy Code is required for every row.");
                if (string.IsNullOrWhiteSpace(r.AccountCode)) return (false, "Account Code is required for every row.");
                if (string.IsNullOrWhiteSpace(r.ProdLevyType)) return (false, "Customer/Levy Type is required for every row.");
                if (string.IsNullOrWhiteSpace(r.DbcrFlag)) return (false, "DBCR Flag is required for every row.");

                if (r.ProdPrefix.Trim().Length > PrefixLen)
                    return (false, $"Prefix '{r.ProdPrefix.Trim()}' exceeds {PrefixLen} characters.");
                if (r.AccountCode.Trim().Length > AccountLen)
                    return (false, $"Account code '{r.AccountCode.Trim()}' exceeds {AccountLen} characters.");

                var key = r.ProdPrefix.Trim() + "||" + r.AccountCode.Trim();
                if (!seen.Add(key))
                    return (false, $"Duplicate row: {r.ProdPrefix.Trim()} / {r.AccountCode.Trim()}");
            }

            await using var tran = await _context.Database.BeginTransactionAsync();
            try
            {
                // INSERT ONLY. The previous version deleted every row for the
                // scope and reinserted the payload, which forced the UI to load
                // all 1711 Sales/Domestic rows before Add so nothing was lost.
                // Add mode now posts only the new rows, so existing rows are
                // never touched. Reject anything that already exists instead.
                foreach (var r in model.Rows)
                {
                    // The account must exist. GetLinksAsync inner-joins
                    // cfn_account, so a typo would save successfully and then
                    // never appear in the grid. Eleven account codes contain a
                    // space (e.g. 'I 010100' = SALES - LOCAL (OEM)), which makes
                    // hand-typed codes easy to get wrong.
                    var accountOk = await _context.CfnAccounts
                        .AsNoTracking()
                        .AnyAsync(a => a.Accountcode == r.AccountCode.Trim()
                                    && a.Accountstatus != "OBSLT");

                    if (!accountOk)
                    {
                        await tran.RollbackAsync();
                        return (false,
                            $"Account code '{r.AccountCode.Trim()}' does not exist " +
                            $"or is obsolete. Pick it from the list rather than typing it - " +
                            $"some codes contain a space.");
                    }

                    var exists = await _context.CfnAccountottolinks
                        .AsNoTracking()
                        .AnyAsync(x => x.ProdPrefix == r.ProdPrefix.Trim()
                                    && x.Accountcode == r.AccountCode.Trim()
                                    && x.VchrType == vchr
                                    && x.ProdPrefixtype == ptype);

                    if (exists)
                    {
                        await tran.RollbackAsync();
                        return (false,
                            $"'{r.ProdPrefix.Trim()}' is already linked to account " +
                            $"'{r.AccountCode.Trim()}' for this voucher and prefix type.");
                    }
                }

                // Column set matches what the legacy screen writes, verified
                // against existing rows:
                //   ctrl_onholdno  -> NULL on every row, including ones the
                //                     legacy app created. Not used by this table.
                //   ctrl_prevrefr  -> NULL on every row.
                //   ctrl_accperiod -> BLANK STRING on legacy rows, not NULL.
                //                     Omitting it produced NULL and made new
                //                     rows differ from every existing one.
                const string insertSql = @"
                    INSERT INTO dbo.cfn_accountottolink
                        (prod_prefix, accountcode, vchr_type, prod_prefixtype,
                         prod_levytype, dbcrflag, ctrl_status, ctrl_cancelflag,
                         ctrl_locationcode, ctrl_accperiod, ctrl_username, ctrl_createdon,
                         ctrl_lastupdate, ctrl_logextract, ctrl_trglocationcode,
                         ctrl_logextracttype, ctrl_nextrefrflag, objectstatus)
                    VALUES
                        (@prefix, @account, @vchr, @ptype,
                         @levy, @dbcr, @status, 'N',
                         @loc, '', @user, @now,
                         @now, 'N', @loc,
                         'N', 'N', 'ACTVE');";

                foreach (var r in model.Rows)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        insertSql,
                        VarChar("@prefix", r.ProdPrefix.Trim(), PrefixLen),
                        VarChar("@account", r.AccountCode.Trim(), AccountLen),
                        VarChar("@vchr", vchr, 1),
                        VarChar("@ptype", ptype, 1),
                        VarChar("@levy", r.ProdLevyType.Trim(), 1),
                        VarChar("@dbcr", r.DbcrFlag.Trim(), 1),
                        VarChar("@status", status, 5),
                        VarChar("@loc", loc, 5),
                        VarChar("@user", user, 30),
                        new SqlParameter("@now", SqlDbType.DateTime) { Value = now });
                }

                await BumpReferenceCounterAsync(model.Rows.Count);

                await tran.CommitAsync();

                var verb = status == "Post" ? "posted" : "put on hold";
                return (true, $"{model.Rows.Count} new prefix link(s) {verb} successfully.");
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                var inner = ex.InnerException?.Message ?? ex.Message;
                return (false, "Error while saving prefix links: " + inner);
            }
        }

        public async Task<(bool Success, string Message)> DeleteLinkAsync(DeletePrefixLinkRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.ProdPrefix) || string.IsNullOrWhiteSpace(model.AccountCode))
                return (false, "Prefix and Account Code are required.");

            try
            {
                var affected = await _context.Database.ExecuteSqlRawAsync(
                    @"DELETE FROM dbo.cfn_accountottolink
                      WHERE prod_prefix     = @prefix
                        AND accountcode     = @account
                        AND vchr_type       = @vchr
                        AND prod_prefixtype = @ptype;",
                    VarChar("@prefix", model.ProdPrefix.Trim(), PrefixLen),
                    VarChar("@account", model.AccountCode.Trim(), AccountLen),
                    VarChar("@vchr", model.VchrType.Trim(), 1),
                    VarChar("@ptype", model.PrefixType.Trim(), 1));

                return affected == 0
                    ? (false, "Row not found.")
                    : (true, "Row deleted successfully.");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                return (false, "Error while deleting row: " + inner);
            }
        }

        public void InvalidateLookupCache()
        {
            _cache.Remove(CacheKeyAccounts);
            _cache.Remove(CacheKeyCustomers);
        }

        /* ==================== HELPERS ==================== */

        /// <summary>
        /// Bumps onholdnumber AND postednumber on cfn_referencectrl for
        /// referencetype = 'VENDR'.
        ///
        /// This matches the legacy commit block exactly. In the legacy flow a
        /// document goes on hold and then posts, so a commit advances both
        /// counters together. The Define Prefix screen has no On Hold step -
        /// it was greyed out in the legacy toolbar, and cfn_accountottolink has
        /// never held an on-hold row - so Post is the only path here.
        /// </summary>
        private async Task BumpReferenceCounterAsync(int count)
        {
            if (count <= 0) return;

            await _context.Database.ExecuteSqlRawAsync(
                @"UPDATE dbo.cfn_referencectrl
                  SET onholdnumber = ISNULL(onholdnumber, 0) + @n,
                      postednumber = ISNULL(postednumber, 0) + @n
                  WHERE referencetype = @type;",
                new SqlParameter("@n", SqlDbType.Int) { Value = count },
                VarChar("@type", ReferenceType, 10));
        }

        private static void AddScopeParams(System.Data.Common.DbCommand cmd,
                                           string vchr, string ptype, string? term)
        {
            cmd.Parameters.Add(VarChar("@vchr", vchr.Trim(), 1));
            cmd.Parameters.Add(VarChar("@ptype", ptype.Trim(), 1));
            cmd.Parameters.Add(new SqlParameter("@term", SqlDbType.VarChar, 100)
            {
                Value = (object?)term ?? DBNull.Value
            });
            cmd.Parameters.Add(new SqlParameter("@like", SqlDbType.VarChar, 102)
            {
                Value = term is null ? DBNull.Value : "%" + term + "%"
            });
        }

        /// <summary>
        /// Explicit VarChar parameter. The columns are char/varchar, so an
        /// NVarChar parameter forces CONVERT_IMPLICIT on the column and kills
        /// the index seek.
        /// </summary>
        private static SqlParameter VarChar(string name, string? value, int size) =>
            new(name, SqlDbType.VarChar, size)
            {
                Value = (object?)value ?? DBNull.Value
            };

        private static string Str(System.Data.Common.DbDataReader r, int i) =>
            r.IsDBNull(i) ? string.Empty : r.GetString(i);

        private static string? Trunc(string? value, int maxLen) =>
            value is null ? null : (value.Length > maxLen ? value[..maxLen] : value);
    }
}