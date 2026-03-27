using Steam.Shared.Constans;
using System.ComponentModel.DataAnnotations;

namespace Steam.Application.Models.Request
{
    public class UpdateUserRequest
    {
        [MaxLength(150, ErrorMessage = ValidatioConstans.MAX_LENGTH)]
        [MinLength(2, ErrorMessage = ValidatioConstans.MIN_LENGTH)]
        public string Nombre { get; set; }

        [MaxLength(150, ErrorMessage = ValidatioConstans.MAX_LENGTH)]
        [MinLength(2, ErrorMessage = ValidatioConstans.MIN_LENGTH)]
        public string Correo { get; set; }

        [MaxLength(150, ErrorMessage = ValidatioConstans.MAX_LENGTH)]
        [MinLength(2, ErrorMessage = ValidatioConstans.MIN_LENGTH)]
        public string Password { get; set; }

        [MaxLength(150, ErrorMessage = ValidatioConstans.MAX_LENGTH)]
        [MinLength(2, ErrorMessage = ValidatioConstans.MIN_LENGTH)]

        public string Pais { get; set; }
    }
}
