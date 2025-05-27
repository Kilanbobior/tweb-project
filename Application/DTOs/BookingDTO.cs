using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BookingDTO
    {
        public Guid Id { get; set; }
        public Guid VacationBaseId { get; set; }
        public string VacationBaseName { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
