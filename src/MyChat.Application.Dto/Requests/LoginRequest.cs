using System.ComponentModel.DataAnnotations;

namespace MyChat.Application.Dto.Requests
{
    public class LoginRequest
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember-Me")]
        public bool RememberMe { get; set; }
    }
}
