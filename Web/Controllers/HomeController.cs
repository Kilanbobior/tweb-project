using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Services;
using VacationBooking.Web.Models;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IVacationBaseService vacationBaseService;
        
        public HomeController()
        {
            vacationBaseService = new VactionBaseService();
        }
        
        
        
        public ActionResult Index()
        {
            var vacationBaseService = new VactionBaseService();

            // Get all available accommodation options
            var accommodationOptions = vacationBaseService.GetAllVacationBases();

            // Create search model for the filter form
            var searchModel = new SearchViewModel
            {
                CheckInDate = DateTime.Today.AddDays(1),
                CheckOutDate = DateTime.Today.AddDays(8),
                Capacity = 2
            };

            // Create a combined view model
            var viewModel = new HomeViewModel
            {
                SearchModel = searchModel,
                AccommodationOptions = accommodationOptions
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            

            // Filter options by date and capacity only
            var results = vacationBaseService.FilterAccommodationOptions(
                model.CheckInDate,
                model.CheckOutDate,
                model.Capacity);

            return View("AccommodationOptions", results);
        }
    }
}