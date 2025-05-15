using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Dto;
using WebApplication.Models;

namespace WebApplication.Services
{
    internal interface IRentService
    {
        ActionResult Rent(RentRequest request);

    }


    public class RentService : IRentService
    {
        private readonly TWebDbContext _db;

        public RentService()
        {
            _db = new TWebDbContext();
        }


        public ActionResult Rent(RentRequest request)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == request.UserId);
            var product = _db.Products.FirstOrDefault(x => x.Id == request.ProductId);
            if (user == null || product == null)
            {
                return new HttpNotFoundResult();
            }


            var rent = new Order
            {
                Price = product.Price * (request.StartDate - request.EndDate).Days,
                ProductName = product.Title,
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ProductId = request.ProductId
            };

            _db.Orders.Add(rent);
            _db.SaveChanges();

            return new RedirectResult("$/Home/Index");
        }


        


    }
}
