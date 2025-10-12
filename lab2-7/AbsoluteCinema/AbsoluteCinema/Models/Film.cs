using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbsoluteCinema.Models
{
    public class Film
    {
        [Key]
        public long FilmID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Genre { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Director { get; set; } = string.Empty;

        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        [Range(1, 600)]
        public int DurationMinutes { get; set; }

        [MaxLength(300)]
        public string PosterUrl { get; set; } = string.Empty;

        [Column(TypeName = "decimal(8, 2)")]
        [Range(0, 9999)]
        public decimal TicketPrice { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
