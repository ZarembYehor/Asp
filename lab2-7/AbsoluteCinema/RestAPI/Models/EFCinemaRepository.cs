using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RestAPI.Models
{
    public class EFCinemaRepository : ICinemaRepository
    {
        private readonly CinemaDbContext context;

        public EFCinemaRepository(CinemaDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Film> Films => context.Films;
        public IQueryable<Screening> Screenings => context.Screenings.Include(s => s.Film);
        public void CreateFilm(Film film)
        {
            context.Add(film);
            context.SaveChanges();
        }

        public void DeleteFilm(Film film)
        {
            context.Remove(film);
            context.SaveChanges();
        }

        public void SaveFilm(Film film)
        {
            if (film.FilmID == 0)
            {
                context.Add(film);
            }
            else
            {
                context.Update(film);
            }
            context.SaveChanges();
        }

        public void CreateScreening(Screening screening)
        {
            context.Add(screening);
            context.SaveChanges();
        }

        public void DeleteScreening(Screening screening)
        {
            context.Remove(screening);
            context.SaveChanges();
        }

        public void SaveScreening(Screening screening)
        {
            if (screening.ScreeningID == 0)
            {
                context.Add(screening);
            }
            else
            {
                if (screening.Film != null)
                {
                    context.Entry(screening.Film).State = EntityState.Unchanged;
                }

                context.Screenings.Attach(screening);

                context.Entry(screening).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}