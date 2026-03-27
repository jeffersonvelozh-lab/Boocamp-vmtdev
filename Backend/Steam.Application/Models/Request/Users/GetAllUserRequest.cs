namespace Steam.Application.Models.Request.Users
{
    public class GetAllUserRequest
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }

        public string Pais { get; set; }
    }
}
