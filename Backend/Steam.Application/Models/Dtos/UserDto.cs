namespace Steam.Application.Models.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public required string Correo { get; set; }
        public required string Password { get; set; }
        public string? Country { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
