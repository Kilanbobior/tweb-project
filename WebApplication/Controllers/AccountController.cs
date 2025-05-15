using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Dto;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly TWebDbContext _webDbContext;


        public AccountController()
        {
            _accountService = new AccountService();
            _webDbContext = new TWebDbContext();
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Edit_prof()
        {
            return View();
        }

        public ActionResult Edit_prof(User _model)
        {
            var model = _accountService.EditProfil(_model);
            return View();
        }


        public ActionResult Profil(string name)
        {
            var user = _webDbContext.Users.FirstOrDefault(x => x.Username == name);
            if (user == null)
            {
                return HttpNotFound();
            }
           
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public ActionResult Login(LoginRequest model)
        {
            var result = _accountService.Login(model);
            if (result == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return new RedirectResult(result);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(RegisterRequest model)
        {
            return _accountService.Register(model);
        }

        public ActionResult SignUp()
        {
            return View();
        }


        public ActionResult History()
        {
            var id = Guid.Parse(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData.Split(',')[0]);
            var rents = _webDbContext.Orders.Where(x => x.UserId == id);
            return View(rents);
        }


    }
}
