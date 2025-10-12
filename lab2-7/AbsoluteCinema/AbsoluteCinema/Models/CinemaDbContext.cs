using Microsoft.EntityFrameworkCore;

namespace AbsoluteCinema.Models
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }

        public DbSet<Film> Films => Set<Film>();
    }
}
