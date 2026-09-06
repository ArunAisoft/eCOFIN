namespace eCOFIN.Application.Interfaces.Login
{
    using eCOFIN.Application.DTOs.Login;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserWithPermissionsDto?> ValidateUserAsync(string username, string password);
    }
}