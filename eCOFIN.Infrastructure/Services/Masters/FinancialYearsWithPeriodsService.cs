using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Application.Interfaces.Vouchers;
using eCOFIN.Infrastructure.BackgroundJobs;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class FinancialYearsWithPeriodsService : IFinancialYearsWithPeriodsService
    {
        private readonly BilzFinDbContext _context;
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<FinancialYearsWithPeriodsService> _logger;

        public FinancialYearsWithPeriodsService(BilzFinDbContext context, IBackgroundTaskQueue taskQueue, IServiceScopeFactory scopeFactory, ILogger<FinancialYearsWithPeriodsService> logger)
        {
            _context = context;
            _taskQueue = taskQueue;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<FinancialYearsWithPeriodsDto>> GetFinancialYearPeriodsAsync()
        {
            try
            {
                var query = from y in _context.CfnFinancialyears.AsNoTracking()
                            join p in _context.CfnAccncalenders.AsNoTracking()
                            on y.Financialyear equals p.Financialyear
                            orderby y.Financialyear, p.Sequence
                            select new
                            {
                                y.Financialyear,
                                y.Fromdate,
                                y.Todate,
                                y.Description,
                                Period = new FinancialYearPeriodsDto
                                {
                                    Financialyear = y.Financialyear,
                                    Accperiod = p.Accperiod,
                                    Accmonth = p.Accmonth,
                                    Accyear = p.Accyear,
                                    Periodfrom = p.Periodfrom,
                                    Periodto = p.Periodto,
                                    Sequence = p.Sequence
                                }
                            };

                var rows = await query.ToListAsync();

                var grouped = rows
                    .GroupBy(x => new { x.Financialyear, x.Fromdate, x.Todate, x.Description })
                    .Select(g => new FinancialYearsWithPeriodsDto
                    {
                        Financialyear = g.Key.Financialyear,
                        Fromdate = g.Key.Fromdate,
                        Todate = g.Key.Todate,
                        Description = g.Key.Description,
                        Periods = g.Select(x => x.Period)
                                         .OrderByDescending(p => p.Periodto)
                                         .ToList()
                    })
                    .OrderByDescending(g => int.Parse(g.Financialyear))
                    .ToList();

                return grouped;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Financial Year & Periods: " + ex.Message);
            }
        }

        public async Task<IEnumerable<FinancialYearDto>> GetFinancialYearsAsync()
        {
            try
            {
                return await _context.CfnFinancialyears
                    .AsNoTracking()
                    .OrderByDescending(x => x.Fromdate)
                    .Select(x => new FinancialYearDto
                    {
                        FinancialYear = x.Financialyear,
                        FromDate = x.Fromdate,
                        ToDate = x.Todate,
                        Description = x.Description
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Financial Years (Apr-Mar): " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> CreateFinancialYearAsync(FinancialYearCreateModel model)
        {
            try
            {
                var exists = await _context.CfnFinancialyears.AnyAsync(x => x.Financialyear == model.financialYear);
                if (exists)
                    return (false, $"Financial Year '{model.financialYear}' already exists.");

                var entity = new CfnFinancialyear
                {
                    Financialyear = model.financialYear,
                    Fromdate = DateTime.Parse(model.fromDate),
                    Todate = DateTime.Parse(model.toDate),
                    Description = model.description,
                    CtrlStatus = "Post",
                    CtrlCreatedon = DateTime.Now,
                    CtrlLastupdate = DateTime.Now,
                    CtrlLocationcode = model.location,
                    CtrlUsername = model.username
                };

                _context.CfnFinancialyears.Add(entity);
                await _context.SaveChangesAsync();

                return (true, "Financial Year created successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Error while creating Financial Year: " + ex.Message);
            }
        }

        public async Task<IEnumerable<FinancialYearDto>> GetFinancialYears2Async()
        {
            try
            {
                return await _context.CfnFinancialyear2s
                    .AsNoTracking()
                    .OrderByDescending(x => x.Fromdate)
                    .Select(x => new FinancialYearDto
                    {
                        FinancialYear = x.Financialyear,
                        FromDate = x.Fromdate,
                        ToDate = x.Todate,
                        Description = x.Description
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Financial Years (Jan-Dec): " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> CreateFinancialYear2Async(FinancialYearCreateModel model)
        {
            try
            {
                var exists = await _context.CfnFinancialyear2s.AnyAsync(x => x.Financialyear == model.financialYear);
                if (exists)
                    return (false, $"Financial Year '{model.financialYear}' already exists.");

                var entity = new CfnFinancialyear2
                {
                    Financialyear = model.financialYear,
                    Fromdate = DateTime.Parse(model.fromDate),
                    Todate = DateTime.Parse(model.toDate),
                    Description = model.description,
                    CtrlStatus = "Post",
                    CtrlCreatedon = DateTime.Now,
                    CtrlLastupdate = DateTime.Now,
                    CtrlLocationcode = model.location,
                    CtrlUsername = model.username
                };

                _context.CfnFinancialyear2s.Add(entity);
                await _context.SaveChangesAsync();

                return (true, "Financial Year created successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Error while creating Financial Year: " + ex.Message);
            }
        }

        public async Task<IEnumerable<AccountingPeriodDto>> GetPeriodsByYearAsync(string financialYear)
        {
            try
            {
                return await _context.CfnAccncalenders
                    .Where(x => x.Accyear == financialYear)
                    .OrderByDescending(x => x.Periodfrom)
                    .Select(x => new AccountingPeriodDto
                    {
                        Accperiod = x.Accperiod,
                        Accmonth = x.Accmonth,
                        Periodfrom = x.Periodfrom,
                        Periodto = x.Periodto,
                        Sequence = x.Sequence,
                        Accyear = x.Accyear
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving accounting periods: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdatePeriodAsync(AccountingPeriodCreateModel model)
        {
            try
            {
                var existing = await _context.CfnAccncalenders.FirstOrDefaultAsync(x => x.Sequence == model.Sequence);

                if (existing != null)
                {
                    existing.Periodfrom = DateTime.Parse(model.Periodfrom);
                    existing.Periodto = DateTime.Parse(model.Periodto);
                    existing.Accperiod = model.Accperiod;
                    existing.Accmonth = model.Accmonth;
                    existing.Accyear = model.Accyear;

                    await _context.SaveChangesAsync();
                    return (true, "Accounting period updated successfully.");
                }

                var entity = new CfnAccncalender
                {
                    Financialyear = model.FinancialYear,
                    Accperiod = model.Accperiod,
                    Accmonth = model.Accmonth,
                    Periodfrom = DateTime.Parse(model.Periodfrom),
                    Periodto = DateTime.Parse(model.Periodto),
                    Sequence = model.Sequence.GetValueOrDefault(),
                    Accyear = model.Accyear,
                    Periodstate = "OPNPR",
                    CtrlTrglocationcode = ""
                };

                _context.CfnAccncalenders.Add(entity);
                await _context.SaveChangesAsync();

                var accPeriod = model.Accperiod;
                _taskQueue.QueueTask(async cancellationToken =>
                {
                    try
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var ledgerService = scope.ServiceProvider.GetRequiredService<ILedgerRecalculationService>();
                        await ledgerService.OpenNewPeriodAsync(accPeriod);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Background OpenNewPeriodAsync failed for period {Period}", accPeriod);
                    }
                });

                return (true, "Accounting period created. Ledger carry-forward is running in background.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}