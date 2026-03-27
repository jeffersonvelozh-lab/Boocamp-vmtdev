using Steam.Application.Models.Responses;

namespace Steam.Application.Helpers
{
    public class PesponseHelper
    {
        public static GenericResponse<T> Create<T>(T data, string message = "Solicitud realizada correctamente")
        {
            var response = new GenericResponse<T>
            {
                Data = data,
                Message = message
            };

            return response;
        }
    }
}
