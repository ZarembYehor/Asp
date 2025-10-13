using Microsoft.AspNetCore.Mvc;
using AbsoluteCinema.Helpers;
using AbsoluteCinema.Models;
using System.Linq;

namespace AbsoluteCinema.Controllers
{
    public class BookingController : Controller
    {
        private const string SeatsSessionKey = "SelectedSeats";

        public IActionResult SelectSeat(int row, int number)
        {
            if (BookingDataStore.GetBookedSeats().Any(s => s.Row == row && s.Number == number))
            {
                return Ok(new { success = false, message = "Seat is permanently booked." });
            }

            var seats = HttpContext.Session.GetObject<List<Seat>>(SeatsSessionKey) ?? new List<Seat>();

            var existingSeat = seats.FirstOrDefault(s => s.Row == row && s.Number == number);
            if (existingSeat != null)
            {
                seats.Remove(existingSeat);
            }
            else
            {
                seats.Add(new Seat { Row = row, Number = number });
            }

            HttpContext.Session.SetObject(SeatsSessionKey, seats);
            return Ok(new { success = true, selected = seats });
        }

        public IActionResult GetSelectedSeats()
        {
            var seats = HttpContext.Session.GetObject<List<Seat>>(SeatsSessionKey) ?? new List<Seat>();
            return Json(seats);
        }

        public IActionResult Confirm()
        {
            var seats = HttpContext.Session.GetObject<List<Seat>>(SeatsSessionKey) ?? new List<Seat>();

            if (seats.Any())
            {
                BookingDataStore.AddPermanentBookings(seats);

                HttpContext.Session.Remove(SeatsSessionKey);
                TempData["Message"] = $"Booking confirmed for {seats.Count} seat(s)!";
            }
            else
            {
                TempData["Message"] = "No seats were selected for booking.";
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SelectSeats()
        {
            return View();
        }
    }
}