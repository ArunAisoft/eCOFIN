using eCOFIN.Application.DTOs.Masters;

namespace eCOFIN.Application.Interfaces.Masters
{
    public interface IUserPermissionService
    {
        Task<List<UserListDto>> GetAllUsersAsync();
        Task<List<PermissionSummaryDto>> GetAllPermissionsAsync();
        Task<List<TaskDto>> GetTasksAsync();
        Task<List<PanelDto>> GetPanelsAsync(int taskId);
        Task<UserPermissionDto> GetUserPermissionsAsync(string username);
        Task<(bool Success, string Message)> SaveOrUpdatePermissionsAsync(SaveUserPermissionRequest model);
    }
}