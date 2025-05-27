using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SearchViewModel
    {
        [Required]
        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Display(Name = "Number of Guests")]
        [Range(1, 20, ErrorMessage = "Please enter a valid number of guests between 1 and 20.")]
        public int Capacity { get; set; }
    }
}