using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CreateVacationBaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AccommodationType { get; set; } // e.g., "Cabin", "Room", "Suite"
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
    }
}