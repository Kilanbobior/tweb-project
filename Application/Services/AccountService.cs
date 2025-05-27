using BusinessLogic;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using Application.DTOs;


namespace Application.Services
{

    public interface IAccountService
    {
        string Login(LoginModel model);
        ActionResult Register(RegisterModel model);
        UserDto GetProfile();
        UserDto GetUser(string username);
    }

    public class AccountService : IAccountService
    {
        private readonly TWebDbContext _context;

        public AccountService()
        {
            _context = new TWebDbContext();
        }

        public string Login(LoginModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == model.Username);
            if (user == null)
            {
                return "/Home/Index";
            }

            if (!PasswordHelper.VerifyPassword(model.Password, user.PasswordHash))
            {
                return null;
            }


            var userData = $"{user.Id}, {user.Role}";

            var ticket = new FormsAuthenticationTicket(
                1,
                user.Name,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                true,
                userData,
                FormsAuthentication.FormsCookiePath
            );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL
            };
            HttpContext.Current.Response.Cookies.Add(authCookie);
            return "/Home/Index";
        }

        public ActionResult Register(RegisterModel model)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Role = "User",
                Name = model.Username,
                Email = model.Email,
                PasswordHash = PasswordHelper.HashPassword(model.Password),
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return new RedirectResult("/Home/Index");
        }

        public UserDto GetProfile()
        {
            var userName = HttpContext.Current.User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Name == userName);
            if (user == null)
            {
                return null;
            }

            var ans = new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Bookings = user.Bookings,
                PhoneNumber = user.PhoneNumber,
            };

            return ans;
        }


        public UserDto GetUser(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == username);

            var ans = new UserDto
            {
                Name = username,
                Email = user.Email,
                Role = user.Role,
                Bookings = user.Bookings,
                PhoneNumber = user.PhoneNumber,
            };

            return ans;
        }

    }
}
