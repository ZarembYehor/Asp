using AbsoluteCinema.Models;
using System.Collections.Generic;
using System.Linq;

namespace AbsoluteCinema.Models
{
    public static class BookingDataStore
    {
        private static readonly List<Seat> PermanentBookedSeats = new List<Seat>
        {
            new Seat { Row = 1, Number = 3 },
            new Seat { Row = 2, Number = 1 },
            new Seat { Row = 2, Number = 2 }
        };

        public static IEnumerable<Seat> GetBookedSeats() => PermanentBookedSeats;
        public static void AddPermanentBookings(IEnumerable<Seat> seats)
        {
            foreach (var seat in seats)
            {
                if (!PermanentBookedSeats.Any(s => s.Row == seat.Row && s.Number == seat.Number))
                {
                    PermanentBookedSeats.Add(seat);
                }
            }
        }
    }
}