using eCOFIN.Application.DTOs.Masters;

namespace eCOFIN.Application.Interfaces.Masters
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<(bool Success, string Message)> SaveOrUpdateUserAsync(UserCreateModel model);
    }
}
