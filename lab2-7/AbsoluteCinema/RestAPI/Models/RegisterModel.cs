using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Будь ласка, введіть Email.")]
        [EmailAddress]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть пароль.")]
        [StringLength(100, ErrorMessage = "Пароль повинен бути не коротший за {2} символів.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}