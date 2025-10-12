using Microsoft.EntityFrameworkCore;

namespace AbsoluteCinema.Models
{
    public class EFCinemaRepository : ICinemaRepository
    {
        private readonly CinemaDbContext context;

        public EFCinemaRepository(CinemaDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Film> Films => context.Films;
    }
}