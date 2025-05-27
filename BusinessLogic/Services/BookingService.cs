using Domain.Entities;
using Domain.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{

    public interface IBookingService
    {
        bool IsVacationBaseAvailable(Guid vacationBaseId, DateTime checkInDate, DateTime checkOutDate);
        Booking CreateBooking(Guid vacationBaseId, Guid userId, DateTime checkInDate, DateTime checkOutDate);
        void ConfirmBooking(Guid bookingId);
        void CancelBooking(Guid bookingId);
    }

    public class BookingService : IBookingService
    {
        public bool IsVacationBaseAvailable(Guid vacationBaseId, DateTime checkInDate, DateTime checkOutDate)
        {
            using (var context = new TWebDbContext())
            {
                var vacationBase = context.VacationBases.Find(vacationBaseId);
                if (vacationBase == null || !vacationBase.IsAvailable)
                    return false;

                // Check if there are any overlapping bookings
                var overlappingBookings = context.Bookings
                    .Where(b => b.VacationBaseId == vacationBaseId &&
                               b.Status != BookingStatus.Cancelled &&
                               ((b.CheckInDate <= checkInDate && b.CheckOutDate > checkInDate) ||
                                (b.CheckInDate < checkOutDate && b.CheckOutDate >= checkOutDate) ||
                                (b.CheckInDate >= checkInDate && b.CheckOutDate <= checkOutDate)))
                    .Any();

                return !overlappingBookings;
            }
        }

        public Booking CreateBooking(Guid vacationBaseId, Guid userId, DateTime checkInDate, DateTime checkOutDate)
        {
            if (!IsVacationBaseAvailable(vacationBaseId, checkInDate, checkOutDate))
                throw new InvalidOperationException("Vacation base is not available for the selected dates.");

            using (var context = new TWebDbContext())
            {
                var vacationBase = context.VacationBases.Find(vacationBaseId);
                int numberOfNights = (int)(checkOutDate - checkInDate).TotalDays;
                decimal totalPrice = vacationBase.PricePerNight * numberOfNights;

                var booking = new Booking
                {
                    VacationBaseId = vacationBaseId,
                    UserId = userId,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    TotalPrice = totalPrice,
                    Status = BookingStatus.Pending
                };

                context.Bookings.Add(booking);
                context.SaveChanges();

                return booking;
            }
        }

        public void ConfirmBooking(Guid bookingId)
        {
            using (var context = new TWebDbContext())
            {
                var booking = context.Bookings.Find(bookingId);
                if (booking == null)
                    throw new InvalidOperationException("Booking not found.");

                booking.Status = BookingStatus.Confirmed;
                context.SaveChanges();
            }
        }

        public void CancelBooking(Guid bookingId)
        {
            using (var context = new TWebDbContext())
            {
                var booking = context.Bookings.Find(bookingId);
                if (booking == null)
                    throw new InvalidOperationException("Booking not found.");

                booking.Status = BookingStatus.Cancelled;
                context.SaveChanges();
            }
        }
    }
}
