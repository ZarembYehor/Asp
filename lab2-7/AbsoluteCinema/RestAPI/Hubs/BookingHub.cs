using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace RestAPI.Hubs
{
    public class BookingHub : Hub
    {
        private static readonly ConcurrentDictionary<int, bool> BookedSeats = new();

        private static readonly ConcurrentDictionary<int, string> LockedSeats = new();

        public async Task BookSeats(List<int> seatNumbers)
        {
            foreach (var seat in seatNumbers)
            {
                BookedSeats[seat] = true;
                LockedSeats.TryRemove(seat, out _);
            }

            await Clients.All.SendAsync("SeatsUpdated", GetAllSeatStates());
        }

        public async Task LockSeats(List<int> seatNumbers)
        {
            var connectionId = Context.ConnectionId;

            foreach (var seat in seatNumbers)
            {
                if (!BookedSeats.ContainsKey(seat))
                {
                    LockedSeats[seat] = connectionId;
                }
            }

            await Clients.All.SendAsync("SeatsUpdated", GetAllSeatStates());
        }

        public async Task UnlockSeats(List<int> seatNumbers)
        {
            var connectionId = Context.ConnectionId;

            foreach (var seat in seatNumbers)
            {
                if (LockedSeats.TryGetValue(seat, out var lockerId) && lockerId == connectionId)
                {
                    LockedSeats.TryRemove(seat, out _);
                }
            }

            await Clients.All.SendAsync("SeatsUpdated", GetAllSeatStates());
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            var seatsToUnlock = LockedSeats
                .Where(s => s.Value == connectionId)
                .Select(s => s.Key)
                .ToList();

            foreach (var seat in seatsToUnlock)
            {
                LockedSeats.TryRemove(seat, out _);
            }

            await Clients.All.SendAsync("SeatsUpdated", GetAllSeatStates());
            await base.OnDisconnectedAsync(exception);
        }

        public static List<SeatState> GetAllSeatStates()
        {
            return Enumerable.Range(1, 30).Select(seatNumber =>
            {
                if (BookedSeats.ContainsKey(seatNumber))
                    return new SeatState(seatNumber, "booked");
                if (LockedSeats.ContainsKey(seatNumber))
                    return new SeatState(seatNumber, "locked");
                return new SeatState(seatNumber, "free");
            }).ToList();
        }
    }

    public record SeatState(int Number, string Status);
}
