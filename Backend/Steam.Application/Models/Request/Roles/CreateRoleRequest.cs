using Steam.Shared.Constans;
using System.ComponentModel.DataAnnotations;

namespace Steam.Application.Models.Request.Roles
{
    public class CreateRoleRequest
    {
        [Required]
        [MaxLength(150, ErrorMessage = ValidatioConstans.MAX_LENGTH)]
        [MinLength(2, ErrorMessage = ValidatioConstans.MIN_LENGTH)]
        public required string RoleName { get; set; }
    }
}
