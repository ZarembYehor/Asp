using System.ComponentModel.DataAnnotations;

namespace AbsoluteCinema.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введіть Email")]
        [EmailAddress(ErrorMessage = "Некоректний формат Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
