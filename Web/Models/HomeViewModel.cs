using Application.DTOs;
using System.Collections.Generic;
using Web.Models;

namespace VacationBooking.Web.Models
{
    public class HomeViewModel
    {
        public SearchViewModel SearchModel { get; set; }
        public List<VacationBaseDTO> AccommodationOptions { get; set; }
    }
}