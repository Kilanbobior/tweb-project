using System;
using System.Web.Mvc;
using Application.DTOs;
using System.Web.Security;
using Application.Services;
using VacationBooking.Web.Models;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;

        public AccountController()
        {
            _accountService = new AccountService();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var result = _accountService.Login(model);
            if (result == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return new RedirectResult(result);
        }


        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            return _accountService.Register(model);
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult GetProfile()
        {
            var user = _accountService.GetProfile();

            return View(user);
        }


        public ActionResult AdminPanel()
        {
            return View();
        }


    }
}