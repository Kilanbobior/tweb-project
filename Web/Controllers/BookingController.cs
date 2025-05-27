using System;
using System.Web.Mvc;
using Application.DTOs;
using Application.Services;
using Web.Models;

namespace Web.Controllers
{
    public class BookingController : Controller
    {
        public ActionResult Create(Guid vacationBaseId, DateTime checkInDate, DateTime checkOutDate)
        {
            // Create service directly
            var vacationBaseService = new VactionBaseService();
            var vacationBase = vacationBaseService.GetVacationBaseById(vacationBaseId);
            if (vacationBase == null)
            {
                return HttpNotFound();
            }

            var model = new CreateBookingViewModel
            {
                VacationBaseId = vacationBaseId,
                VacationBaseName = vacationBase.Name,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                TotalNights = (int)(checkOutDate - checkInDate).TotalDays,
                PricePerNight = vacationBase.PricePerNight,
                TotalPrice = vacationBase.PricePerNight * (int)(checkOutDate - checkInDate).TotalDays
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // In a real application, you would get the user ID from the authentication system
                Guid userId = Guid.NewGuid(); // Placeholder

                var bookingRequest = new BookingRequestDTO
                {
                    VacationBaseId = model.VacationBaseId,
                    UserId = userId,
                    CheckInDate = model.CheckInDate,
                    CheckOutDate = model.CheckOutDate
                };

                // Create service directly
                var bookingService = new BookingAppService();
                var booking = bookingService.CreateBooking(bookingRequest);

                return RedirectToAction("Confirmation", new { id = booking.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public ActionResult Confirmation(int id)
        {
            // In a real application, you would get the user's bookings and find the one with the matching ID
            // For simplicity, we'll just return a view
            return View();
        }

        public ActionResult MyBookings()
        {
            // In a real application, you would get the user ID from the authentication system
            Guid userId = Guid.NewGuid(); // Placeholder

            // Create service directly
            var bookingService = new BookingAppService();
            var bookings = bookingService.GetUserBookings(userId);
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Guid id)
        {
            try
            {
                // Create service directly
                var bookingService = new BookingAppService();
                bookingService.CancelBooking(id);
                return RedirectToAction("MyBookings");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("MyBookings");
            }
        }
    }
}