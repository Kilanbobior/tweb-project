    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly TWebDbContext _Context;

        public ProductController()
        {
            _Context = new TWebDbContext();
        }



        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prod_card(Guid id)
        {
            var product = _Context.Products.FirstOrDefault(x => x.Id == id);
            
            if(product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult AddProject(Product product)
        {
            _Context.Products.Add(product);
            _Context.SaveChanges();
            return null;
        }


        public ActionResult AddProject()
        {
            var model = new Product();
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit_product(Guid id)
        {
            return RedirectToAction("Edit_product", "Product");
        }


        public ActionResult Edit_product()
        {
            return View();
        }


        public ActionResult AllProducts()
        {
            return View(_Context.Products.ToList());
        }


    }
}
