using System.ComponentModel.DataAnnotations;

namespace EyeScenceApp.Application.DTOs.ApplicationUser
{
    public class ChangePasswordDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}