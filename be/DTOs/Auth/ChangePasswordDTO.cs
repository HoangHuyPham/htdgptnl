namespace be.DTOs.Auth
{
    public class ChangePasswordDTO
    {
        public string Username { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}