using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCOFIN.Application.DTOs.Login
{
    public class UserLoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserWithPermissionsDto
    {
        public string Username { get; set; }
        public string? NameDescription { get; set; }
        //public string? ObjectStatus { get; set; }
        public List<UserPermissionDto> Permissions { get; set; } = new();
    }
    public class UserPermissionDto
    {
        public string TaskId { get; set; }
        public string LevelNumber { get; set; }
        //public string? ObjectStatus { get; set; }
    }
}
