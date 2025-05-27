using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CreateBookingViewModel
    {
        public Guid VacationBaseId { get; set; }
        public string VacationBaseName { get; set; }

        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public int TotalNights { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal TotalPrice { get; set; }
    }
}