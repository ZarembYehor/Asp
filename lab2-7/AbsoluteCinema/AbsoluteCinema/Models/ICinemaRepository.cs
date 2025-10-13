using System.Linq;

namespace AbsoluteCinema.Models
{
    public interface ICinemaRepository
    {
        IQueryable<Film> Films { get; }
        IQueryable<Screening> Screenings { get; }

        void SaveFilm(Film film);
        void CreateFilm(Film film);
        void DeleteFilm(Film film);
        void SaveScreening(Screening screening);
        void CreateScreening(Screening screening);
        void DeleteScreening(Screening screening);
    }
}