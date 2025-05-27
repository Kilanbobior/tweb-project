using Application.DTOs;
using BusinessLogic;
using BusinessLogic.Services;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{


    public interface IBookingAppService
    {
        BookingDTO CreateBooking(BookingRequestDTO request);
        void ConfirmBooking(Guid bookingId);
        void CancelBooking(Guid bookingId);
        List<BookingDTO> GetUserBookings(Guid userId);
    }


    public class BookingAppService : IBookingAppService
    {
        public BookingDTO CreateBooking(BookingRequestDTO request)
        {
            var bookingService = new BookingService();

            var booking = bookingService.CreateBooking(
                request.VacationBaseId,
                request.UserId,
                request.CheckInDate,
                request.CheckOutDate);

            return MapBookingToDTO(booking);
        }

        public void ConfirmBooking(Guid bookingId)
        {
            var bookingService = new BookingService();
            bookingService.ConfirmBooking(bookingId);
        }

        public void CancelBooking(Guid bookingId)
        {
            var bookingService = new BookingService();
            bookingService.CancelBooking(bookingId);
        }

        public List<BookingDTO> GetUserBookings(Guid userId)
        {
            using (var context = new TWebDbContext())
            {
                return context.Bookings
                    .Include("VacationBase")
                    .Include("User")
                    .Where(b => b.UserId == userId)
                    .Select(b => new BookingDTO
                    {
                        Id = b.Id,
                        VacationBaseId = b.VacationBaseId,
                        VacationBaseName = b.VacationBase.Name,
                        UserId = b.UserId,
                        CheckInDate = b.CheckInDate,
                        CheckOutDate = b.CheckOutDate,
                        TotalPrice = b.TotalPrice,
                        Status = b.Status.ToString()
                    })
                    .ToList();
            }
        }

        private BookingDTO MapBookingToDTO(Booking booking)
        {
            using (var context = new TWebDbContext())
            {
                var vacationBase = context.VacationBases.Find(booking.VacationBaseId);
                var user = context.Users.Find(booking.UserId);

                return new BookingDTO
                {
                    Id = booking.Id,
                    VacationBaseId = booking.VacationBaseId,
                    VacationBaseName = vacationBase.Name,
                    UserId = booking.UserId,
                    CheckInDate = booking.CheckInDate,
                    CheckOutDate = booking.CheckOutDate,
                    TotalPrice = booking.TotalPrice,
                    Status = booking.Status.ToString()
                };
            }
        }
    }
}
