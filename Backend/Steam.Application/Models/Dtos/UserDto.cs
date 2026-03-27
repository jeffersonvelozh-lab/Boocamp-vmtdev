namespace Steam.Application.Models.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
