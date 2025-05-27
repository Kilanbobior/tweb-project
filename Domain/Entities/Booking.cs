using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid VacationBaseId { get; set; }
        public VacationBase VacationBase { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
    }
}
