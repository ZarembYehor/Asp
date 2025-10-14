using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using System.Linq;
using System.Collections.Generic;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        [HttpGet("Seats")]
        public IActionResult GetBookedSeats()
        {
            return Ok(BookingDataStore.GetBookedSeats());
        }

        [HttpPost("Confirm")]
        public IActionResult ConfirmBooking([FromBody] List<Seat> seats)
        {
            if (seats == null || !seats.Any())
            {
                return BadRequest(new { message = "No seats provided for booking." });
            }

            BookingDataStore.AddPermanentBookings(seats);

            return Ok(new { message = $"Booking confirmed for {seats.Count} seat(s)!", seats });
        }
    }
}