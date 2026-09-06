using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class UserService : IUserService
    {
        private readonly BilzFinDbContext _context;

        public UserService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            try
            {
                return await _context.CfnUsers
                    .AsNoTracking()
                    .OrderBy(x => x.Username)
                    .Select(x => new UserDto
                    {
                        Username = x.Username,
                        NameDescription = x.Namedescription ?? "",
                        ObjectStatus = x.CtrlStatus ?? "ACTVE"
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving users: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateUserAsync(UserCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Username))
                    return (false, "Username is required.");

                if (string.IsNullOrWhiteSpace(model.NameDescription))
                    return (false, "Full Name is required.");

                if (model.Password == null)
                    return (false, "Password is required.");

                var status = (model.ObjectStatus ?? "ACTVE").Trim().ToUpper();
                if (status != "ACTVE" && status != "INACT")
                    return (false, "Object Status must be 'ACTVE' or 'INACT'.");

                var uname = model.Username.Trim();

                var existing = await _context.CfnUsers
                    .FirstOrDefaultAsync(x => x.Username == uname);

                if (existing != null)
                {
                    existing.Namedescription = Trunc(model.NameDescription?.Trim(), 100);
                    existing.Password = model.Password.Value;
                    existing.CtrlStatus = status;
                    existing.CtrlLastupdate = DateTime.Now;
                    existing.CtrlUsername = Trunc(model.LoggedInUser, 30);
                    existing.CtrlLocatiocode = Trunc(model.Location, 5);
                    existing.CtrlAccperiod = model.AccPeriod;
                    existing.CtrlNextrefrflag = "N";

                    await _context.SaveChangesAsync();
                    return (true, "User updated successfully.");
                }

                _context.CfnUsers.Add(new CfnUser
                {
                    Username = Trunc(uname, 30)!,
                    Namedescription = Trunc(model.NameDescription?.Trim(), 100),
                    Password = model.Password.Value,
                    Objectstatus = status,
                    CtrlStatus = "Post",
                    CtrlCancelflag = "N",
                    CtrlCreatedon = DateTime.Now,
                    CtrlLastupdate = DateTime.Now,
                    CtrlUsername = Trunc(model.LoggedInUser, 30),
                    CtrlLocatiocode = Trunc(model.Location ?? "BILZ", 5),
                    CtrlTrglocationcode = Trunc(model.Location ?? "BILZ", 5),
                    CtrlLogextract = "N",
                    CtrlLogextracttype = "N",
                    CtrlNextrefrflag = "N"
                });

                await _context.SaveChangesAsync();
                return (true, "User created successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Error while saving user: " + ex.Message);
            }
        }

        private static string? Trunc(string? value, int maxLen) =>
            value == null ? null : (value.Length > maxLen ? value[..maxLen] : value);
    }
}
