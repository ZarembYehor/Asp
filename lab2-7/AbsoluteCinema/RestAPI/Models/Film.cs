using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; 

namespace RestAPI.Models
{
    public class Film
    {
        [Key]
        public long FilmID { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть назву фільму.")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть опис.")]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, вкажіть жанр.")]
        [MaxLength(100)]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, вкажіть режисера.")]
        [MaxLength(100)]
        public string Director { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, вкажіть рік.")]
        [Range(1900, 2100, ErrorMessage = "Рік випуску має бути між 1900 та 2100.")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть тривалість.")]
        [Range(1, 600, ErrorMessage = "Тривалість повинна бути в межах 1-600 хвилин.")]
        public int DurationMinutes { get; set; }

        [MaxLength(300)]
        public string PosterUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, вкажіть ціну квитка.")]
        [Range(0.01, 9999.00, ErrorMessage = "Ціна повинна бути додатною.")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal TicketPrice { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть дату початку показу.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть дату завершення показу.")]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
    }
}