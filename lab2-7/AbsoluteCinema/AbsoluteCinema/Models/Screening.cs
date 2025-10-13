using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbsoluteCinema.Models
{
    public class Screening
    {
        [Key]
        public long ScreeningID { get; set; }

        [Display(Name = "Film")]
        [Required(ErrorMessage = "Будь ласка, виберіть фільм.")]
        public long? FilmID { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть час початку сеансу.")]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть номер залу.")]
        [Range(1, 20, ErrorMessage = "Номер залу має бути в межах 1-20.")]
        public int CinemaHall { get; set; }

        public Film Film { get; set; } = default!;
    }
}