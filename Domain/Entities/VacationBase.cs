using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VacationBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AccommodationType { get; set; } // e.g., "Cabin", "Room", "Suite"
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public string ImageUrl { get; set; } // Add image for each accommodation option
    }
}
