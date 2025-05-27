using System;
using System.Web.Mvc;
using Application.DTOs;
using Application.Services;
using Web.Models;

namespace Web.Controllers
{
    public class VacationBaseController : Controller
    {
        
        private readonly IVacationBaseService vacationBaseService;
        
        public VacationBaseController()
        {
            vacationBaseService = new VactionBaseService();
        }
        
        public ActionResult Index()
        {
            // Create service directly
            var vacationBases = vacationBaseService.GetAllVacationBases();
            return View(vacationBases);
        }

        public ActionResult Details(Guid id)
        {
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
            return vacationBaseService.CreateVacationBase(model);
        }
    }
}