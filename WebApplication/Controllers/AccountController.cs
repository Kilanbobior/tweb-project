using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Dto;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult edit_prof()
        {
            return View();
        }



        // POST: /Account/SingUp
        [HttpPost]
        public ActionResult SignUp(SignUpRequest request)
        {

            //logic will be in lab 5

            return RedirectToAction("Login"); // После регистрации перенаправляем на вход
        }

        // GET: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginRequest request)
        {
            //TODO: lab5

            return View();
        }


        // GET: /Account/Profile
        public ActionResult profile()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }
    }
}