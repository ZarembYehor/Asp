using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RestAPI.Hubs;
using RestAPI.Models;
using System.Linq;
using System.Collections.Generic;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IHubContext<BookingHub> hubContext;

        public BookingController(IHubContext<BookingHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpGet("Seats")]
        public IActionResult GetBookedSeats()
        {
            return Ok(BookingDataStore.GetBookedSeats());
        }

        [HttpPost("Confirm")]
        public async Task<IActionResult> ConfirmBooking([FromBody] List<Seat> seats)
        {
            if (seats == null || !seats.Any())
            {
                return BadRequest(new { message = "No seats provided for booking." });
            }

            BookingDataStore.AddPermanentBookings(seats);

            var updatedSeats = BookingDataStore.GetBookedSeats();
            await hubContext.Clients.All.SendAsync("SeatsUpdated", updatedSeats);

            return Ok(new { message = $"Booking confirmed for {seats.Count} seat(s)!", seats });
        }

        [HttpPost("Cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] List<Seat> seatsToCancel)
        {
            if (seatsToCancel == null || !seatsToCancel.Any())
            {
                return BadRequest(new { message = "No seats provided for cancellation." });
            }

            int removedCount = BookingDataStore.RemoveBookings(seatsToCancel);

            if (removedCount == 0)
            {
                return NotFound(new { message = "Cancellation failed: None of the specified seats were found booked." });
            }

            var updatedSeats = BookingDataStore.GetBookedSeats();
            await hubContext.Clients.All.SendAsync("SeatsUpdated", updatedSeats);

            return Ok(new { message = $"Cancellation successful: {removedCount} seat(s) were released." });
        }
    }
}
