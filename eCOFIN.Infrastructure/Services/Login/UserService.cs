using AutoMapper;
using eCOFIN.Application.DTOs.Login;
using eCOFIN.Application.Interfaces.Login;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Login
{
    public class UserService : IUserService
    {
        private readonly BilzFinDbContext _context;
        public UserService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<UserWithPermissionsDto?> ValidateUserAsync(string username, string password)
        {
            try
            {
                var user = await _context.CfnUsers.FirstOrDefaultAsync(u => u.Username == username && u.Password.ToString() == password);
                if (user == null)
                    return null;

                var permissions = await _context.CfnUserpermissions.AsNoTracking()
                    .Where(p => p.Username == user.Username)
                    .Select(p => new UserPermissionDto
                    {
                        TaskId = p.Taskid,
                        LevelNumber = p.Levelnumber,
                    })
                    .ToListAsync();

                return new UserWithPermissionsDto
                {
                    Username = user.Username,
                    NameDescription = user.Namedescription,
                    Permissions = permissions
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving User with Permissions :" + ex);
            }
        }
    }
}
