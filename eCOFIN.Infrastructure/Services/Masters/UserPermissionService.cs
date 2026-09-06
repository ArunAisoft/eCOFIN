using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly BilzFinDbContext _context;

        public UserPermissionService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserListDto>> GetAllUsersAsync()
        {
            try
            {
                return await _context.CfnUsers
                    .AsNoTracking()
                    .OrderBy(x => x.Username)
                    .Select(x => new UserListDto
                    {
                        Username = x.Username,
                        NameDescription = x.Namedescription ?? ""
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving users: " + ex.Message);
            }
        }

        public async Task<List<PermissionSummaryDto>> GetAllPermissionsAsync()
        {
            try
            {
                // Pull raw data, then group in-memory since Levelnumber is a string in DB
                var userPerms = await (from u in _context.CfnUsers.AsNoTracking()
                                       join up in _context.CfnUserpermissions.AsNoTracking()
                                            on u.Username equals up.Username
                                       select new
                                       {
                                           u.Username,
                                           u.Namedescription,
                                           up.Taskid,
                                           up.Levelnumber
                                       }).ToListAsync();

                var panelCounts = await _context.CfnPanelpermissions
                    .AsNoTracking()
                    .GroupBy(p => p.Username)
                    .Select(g => new { Username = g.Key, Count = g.Select(x => x.Panelid).Distinct().Count() })
                    .ToListAsync();

                var panelMap = panelCounts.ToDictionary(x => x.Username, x => x.Count);

                return userPerms
                    .GroupBy(x => new { x.Username, x.Namedescription })
                    .Select(g => new PermissionSummaryDto
                    {
                        Username = g.Key.Username,
                        NameDescription = g.Key.Namedescription ?? "",
                        LevelNumber = g.Max(x => SafeInt(x.Levelnumber)),
                        TaskCount = g.Select(x => x.Taskid).Distinct().Count(),
                        PanelCount = panelMap.TryGetValue(g.Key.Username, out var c) ? c : 0
                    })
                    .OrderBy(x => x.Username)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving permissions: " + ex.Message);
            }
        }

        public async Task<List<TaskDto>> GetTasksAsync()
        {
            try
            {
                return await _context.CfnTasks
                    .AsNoTracking()
                    .Where(x => (x.Tasktype == "INDPN" || x.Tasktype == "CHILD")
                                && x.Objectstatus == "ACTVE")
                    .OrderBy(x => x.Taskfullname)
                    .Select(static x => new TaskDto
                    {
                        TaskId = int.Parse(x.Taskid),
                        TaskFullName = x.Taskfullname ?? ""
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving tasks: " + ex.Message);
            }
        }

        public async Task<List<PanelDto>> GetPanelsAsync(int taskId)
        {
            try
            {
                return await _context.CfnPanels
                    .AsNoTracking()
                    .Where(x => x.Taskid == taskId.ToString())
                    .OrderBy(x => x.Panelfullname)
                    .Select(x => new PanelDto
                    {
                        PanelId = int.Parse(x.Panelid),
                        PanelFullName = x.Panelfullname ?? ""
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving panels: " + ex.Message);
            }
        }

        public async Task<UserPermissionDto> GetUserPermissionsAsync(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    return new UserPermissionDto();

                var uname = username.Trim();

                // Taskid and Levelnumber are strings in DB — convert to int for the client
                var taskRows = await _context.CfnUserpermissions
                    .AsNoTracking()
                    .Where(x => x.Username == uname)
                    .Select(x => new { x.Taskid, x.Levelnumber })
                    .ToListAsync();

                var panelRows = await _context.CfnPanelpermissions
                    .AsNoTracking()
                    .Where(x => x.Username == uname)
                    .Select(x => x.Panelid)
                    .ToListAsync();

                return new UserPermissionDto
                {
                    TaskIds = taskRows
                        .Select(x => SafeInt(x.Taskid))
                        .Where(v => v > 0)
                        .Distinct()
                        .ToList(),
                    PanelIds = panelRows
                        .Select(SafeInt)
                        .Where(v => v > 0)
                        .Distinct()
                        .ToList(),
                    LevelNumber = taskRows.Count > 0 ? taskRows.Max(x => SafeInt(x.Levelnumber)) : 0
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving user permissions: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdatePermissionsAsync(SaveUserPermissionRequest model)
        {
            using var tran = await _context.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrWhiteSpace(model.Username))
                    return (false, "Username is required.");
                if (model.TaskIds == null || model.TaskIds.Count == 0)
                    return (false, "At least one task must be selected.");

                var uname = model.Username.Trim();
                var levelStr = model.LevelNumber.ToString();
                var loggedIn = Trunc(model.LoggedInUser, 30) ?? uname;
                var loc = Trunc(model.Location ?? "BILZ", 5) ?? "BILZ";
                var now = DateTime.Now;

                var userExists = await _context.CfnUsers.AnyAsync(x => x.Username == uname);
                if (!userExists)
                    return (false, $"User '{uname}' does not exist.");

                // Remove existing permissions for this user (clean-slate save)
                var existingTasks = _context.CfnUserpermissions.Where(x => x.Username == uname);
                _context.CfnUserpermissions.RemoveRange(existingTasks);

                var existingPanels = _context.CfnPanelpermissions.Where(x => x.Username == uname);
                _context.CfnPanelpermissions.RemoveRange(existingPanels);

                await _context.SaveChangesAsync();

                // Insert tasks — fill every audit column that typically has NOT NULL
                foreach (var taskId in model.TaskIds.Distinct())
                {
                    _context.CfnUserpermissions.Add(new CfnUserpermission
                    {
                        Username = uname,
                        Taskid = taskId.ToString(),
                        Levelnumber = levelStr,

                        CtrlStatus = "Post",
                        CtrlCancelflag = "N",
                        CtrlCreatedon = now,
                        CtrlLastupdate = now,
                        CtrlUsername = loggedIn,
                        CtrlLocationcode = loc,
                        CtrlTrglocationcode = loc,
                        CtrlLogextract = "N",
                        CtrlLogextracttype = "N",
                        CtrlNextrefrflag = "N"
                    });
                }

                // Insert panels — only valid (task, panel) pairs that exist in cfn_panel.
                // CfnPanel has int Taskid/Panelid, but CfnPanelpermission stores them as strings.
                if (model.PanelIds != null && model.PanelIds.Count > 0)
                {
                    var taskIds = model.TaskIds.Select(x => x.ToString()).Distinct().ToList();
                    var panelIds = model.PanelIds.Select(x => x.ToString()).Distinct().ToList();

                    var validPairs = await _context.CfnPanels
                        .AsNoTracking()
                        .Where(p => taskIds.Contains(p.Taskid) && panelIds.Contains(p.Panelid))
                        .Select(p => new { p.Taskid, p.Panelid })
                        .ToListAsync();

                    foreach (var pair in validPairs)
                    {
                        _context.CfnPanelpermissions.Add(new CfnPanelpermission
                        {
                            Username = uname,
                            Taskid = pair.Taskid.ToString(),
                            Panelid = pair.Panelid.ToString(),
                            Levelnumber = levelStr,

                            CtrlStatus = "Post",
                            CtrlCancelflag = "N",
                            CtrlCreatedon = now,
                            CtrlLastupdate = now,
                            CtrlUsername = loggedIn,
                            CtrlLocationcode = loc,
                            CtrlTrglocationcode = loc,
                            CtrlLogextract = "N",
                            CtrlLogextracttype = "N",
                            CtrlNextrefrflag = "N"
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await tran.CommitAsync();

                return (true, "Permissions saved successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                await tran.RollbackAsync();
                var inner = dbEx.InnerException?.Message ?? dbEx.Message;
                return (false, "DB error while saving permissions: " + inner);
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                return (false, "Error while saving permissions: " + ex.Message);
            }
        }

        private static string? Trunc(string? value, int maxLen) =>
            value == null ? null : (value.Length > maxLen ? value[..maxLen] : value);

        private static int SafeInt(string? v) =>
            int.TryParse(v, out var n) ? n : 0;
    }
}