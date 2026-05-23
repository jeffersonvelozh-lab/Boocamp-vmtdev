namespace Steam.Application.Models.Responses
{
    public class GenericResponse<T>
    {
        public required string Message { get; set; }
        public DateTime TimeStamp { get; } = DateTimeOffset.UtcNow.DateTime;
        public required T Data { get; set; }
    }
}
