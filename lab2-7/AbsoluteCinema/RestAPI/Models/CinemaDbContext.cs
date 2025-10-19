using Microsoft.EntityFrameworkCore;

namespace RestAPI.Models
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }

        public DbSet<Film> Films => Set<Film>();
        public DbSet<Screening> Screenings => Set<Screening>();
    }
}