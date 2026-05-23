using Steam.Shared.Constans;
using System.ComponentModel.DataAnnotations;

namespace Steam.Application.Models.Request.Users
{
    public class CreateUserRequest
    {
        [Required]
        [MaxLength(150, ErrorMessage = ValidatioConstans.MAX_LENGTH)]
        [MinLength(2, ErrorMessage = ValidatioConstans.MIN_LENGTH)]
        public required string Nombre { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = ValidatioConstans.MAX_LENGTH)]
        [MinLength(2, ErrorMessage = ValidatioConstans.MIN_LENGTH)]
        public required string Correo { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = ValidatioConstans.MAX_LENGTH)]
        [MinLength(2, ErrorMessage = ValidatioConstans.MIN_LENGTH)]
        public required string Password { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = ValidatioConstans.MAX_LENGTH)]
        [MinLength(2, ErrorMessage = ValidatioConstans.MIN_LENGTH)]
        public required string Pais { get; set; }
    }
}
