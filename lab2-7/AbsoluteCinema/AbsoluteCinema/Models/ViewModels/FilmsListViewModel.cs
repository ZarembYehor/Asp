using System.Collections.Generic;

namespace AbsoluteCinema.Models.ViewModels
{
    public class FilmsListViewModel
    {
        public IEnumerable<Film> Films { get; set; } = new List<Film>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public string? CurrentGenre { get; set; }
    }
}