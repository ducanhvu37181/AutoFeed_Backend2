namespace AutoFeed_Backend_Services.DTOs.User
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string Token { get; set; }
    }
}
