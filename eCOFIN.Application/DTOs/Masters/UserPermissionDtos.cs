namespace eCOFIN.Application.DTOs.Masters
{
    public class UserListDto
    {
        public string Username { get; set; } = string.Empty;
        public string NameDescription { get; set; } = string.Empty;
    }

    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskFullName { get; set; } = string.Empty;
    }

    public class PanelDto
    {
        public int PanelId { get; set; }
        public string PanelFullName { get; set; } = string.Empty;
    }

    public class UserPermissionDto
    {
        public int LevelNumber { get; set; }
        public List<int> TaskIds { get; set; } = new();
        public List<int> PanelIds { get; set; } = new();
    }

    public class PermissionSummaryDto
    {
        public string Username { get; set; } = string.Empty;
        public string NameDescription { get; set; } = string.Empty;
        public int LevelNumber { get; set; }
        public int TaskCount { get; set; }
        public int PanelCount { get; set; }
    }

    public class SaveUserPermissionRequest
    {
        public string Username { get; set; } = string.Empty;
        public int LevelNumber { get; set; }
        public List<int> TaskIds { get; set; } = new();
        public List<int> PanelIds { get; set; } = new();
        public string? LoggedInUser { get; set; }
        public string? Location { get; set; }
    }
}