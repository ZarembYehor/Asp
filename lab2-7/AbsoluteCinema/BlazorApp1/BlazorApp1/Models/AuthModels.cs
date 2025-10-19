using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorApp1.Models
{
    public class Film
    {
        public long? FilmID { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть назву фільму.")]
        [StringLength(200, ErrorMessage = "Назва задовга.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть опис.")]
        [StringLength(2000, ErrorMessage = "Опис задовгий.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, вкажіть жанр.")]
        [StringLength(100)]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, вкажіть режисера.")]
        [StringLength(100)]
        public string Director { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, вкажіть рік.")]
        [Range(1900, 2100, ErrorMessage = "Рік випуску має бути між 1900 та 2100.")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть тривалість.")]
        [Range(1, 600, ErrorMessage = "Тривалість повинна бути в межах 1-600 хвилин.")]
        public int DurationMinutes { get; set; }

        public string PosterUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, вкажіть ціну квитка.")]
        [Range(0.01, 9999.00, ErrorMessage = "Ціна повинна бути додатною.")]
        public decimal TicketPrice { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть дату початку показу.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть дату завершення показу.")]
        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
    }

    public class Screening
    {
        public long? ScreeningID { get; set; }

        [Display(Name = "Film")]
        [Required(ErrorMessage = "Будь ласка, виберіть фільм.")]
        public long? FilmID { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть час початку сеансу.")]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть номер залу.")]
        [Range(1, 20, ErrorMessage = "Номер залу має бути в межах 1-20.")]
        public int CinemaHall { get; set; }

        public Film? Film { get; set; } = default!; 
    }

    public class FilmsListViewModel
    {
        public IEnumerable<Film> Films { get; set; } = new List<Film>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public string? CurrentGenre { get; set; }
    }

    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => ItemsPerPage > 0 ? (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage) : 0;
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Введіть Email")]
        [EmailAddress(ErrorMessage = "Некоректний формат Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string User_id { get; set; } = string.Empty;
    }

    public class UserProfile
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }

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

    public class RegisterModel
    {
        [Required(ErrorMessage = "Будь ласка, введіть Email.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть пароль.")]
        [StringLength(100, ErrorMessage = "Пароль повинен бути не коротший за {2} символів.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не збігаються.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}