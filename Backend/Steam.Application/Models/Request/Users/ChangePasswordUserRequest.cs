using Steam.Shared.Constans;
using System.ComponentModel.DataAnnotations;

namespace Steam.Application.Models.Request.Users
{
    public class ChangePasswordUserRequest
    {
        [Required(ErrorMessage = ValidatioConstans.REQUERIDO)]
        public string CurrentPaasswor { get; set; } = null!;
        [Required(ErrorMessage = ValidatioConstans.REQUERIDO)]
        public string NewPassword { get; set; } = null!;
    }
}
