namespace eCOFIN.Application.DTOs.Masters
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string? NameDescription { get; set; }
        public string? ObjectStatus { get; set; }
    }

    public class UserCreateModel
    {
        public string Username { get; set; } = string.Empty;
        public string? NameDescription { get; set; }
        public string? ObjectStatus { get; set; }
        public decimal? Password { get; set; }
        public string? LoggedInUser { get; set; }
        public string? Location { get; set; }
        public string? AccPeriod { get; set; }
    }
}
