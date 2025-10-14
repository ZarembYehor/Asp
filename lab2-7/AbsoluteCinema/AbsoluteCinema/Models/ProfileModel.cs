using System.ComponentModel.DataAnnotations;

namespace AbsoluteCinema.Models.ViewModels
{
    public class ProfileModel
    {
        [Display(Name = "Електронна пошта (не можна змінити)")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Новий пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть новий пароль")]
        [Compare("NewPassword", ErrorMessage = "Нові паролі не збігаються.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Потрібен поточний пароль для збереження змін.")]
        [DataType(DataType.Password)]
        [Display(Name = "Поточний пароль")]
        public string CurrentPassword { get; set; } = string.Empty;
    }
}