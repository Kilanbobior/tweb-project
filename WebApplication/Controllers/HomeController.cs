using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Dto;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _userService;
        private readonly IRentService _rentService;

        public HomeController()
        {
            _rentService = new RentService();
            _userService = new AccountService();
        }



        public ActionResult Basket()
        {

            return View();
        }

 
        public ActionResult index_auto()
        {
            return View();
        }

        
        

        public ActionResult index()
        {

            return View();
        }

        public ActionResult FAQ()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Rent(RentRequest request)
        {
            return _rentService.Rent(request);
        }




    }

}
