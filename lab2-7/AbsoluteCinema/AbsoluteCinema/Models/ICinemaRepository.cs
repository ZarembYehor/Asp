namespace AbsoluteCinema.Models
{
    public interface ICinemaRepository
    {
        IQueryable<Film> Films { get; }
    }

}
