using System;
using System.Web.Mvc;
using Application.DTOs;
using Application.Services;
using Web.Models;

namespace Web.Controllers
{
    public class VacationBaseController : Controller
    {
        public ActionResult Index()
        {
            // Create service directly
            var vacationBaseService = new VactionBaseService();
            var vacationBases = vacationBaseService.GetAllVacationBases();
            return View(vacationBases);
        }

        public ActionResult Details(Guid id)
        {
            // Create service directly
            var vacationBaseService = new VactionBaseService();
            var vacationBase = vacationBaseService.GetVacationBaseById(id);
            if (vacationBase == null)
            {
                return HttpNotFound();
            }
            return View(vacationBase);
        }
        
        
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(VacationBaseDTO model)
        {
            var vacationBaseService = new VactionBaseService();
            return vacationBaseService.CreateVacationBase(model);
        }
    }
}