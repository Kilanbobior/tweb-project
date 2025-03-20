using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult prod_card()
        {
            return View();
        }

        public ActionResult AddProject()
        {

            return null;
        }

        [HttpPost]
        public ActionResult EditProject()
        {
            return null;
        }


    }
}