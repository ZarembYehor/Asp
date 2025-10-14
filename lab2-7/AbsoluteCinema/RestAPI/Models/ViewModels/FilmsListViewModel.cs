using System.Collections.Generic;

namespace RestAPI.Models.ViewModels
{
    public class FilmsListViewModel
    {
        public IEnumerable<Film> Films { get; set; } = new List<Film>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public string? CurrentGenre { get; set; }
    }
}