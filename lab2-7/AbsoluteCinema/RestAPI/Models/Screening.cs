using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; 
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; 

namespace RestAPI.Models
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

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        [ValidateNever]
        public Film Film { get; set; } = default!;
    }
}