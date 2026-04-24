namespace Steam.Application.Models.Request.Users
{
    public class FilterUserRequest : BaseRequest
    {
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }
        public string? Pais { get; set; }
    }
}
