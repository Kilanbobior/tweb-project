using Application.DTOs;
using BusinessLogic;
using BusinessLogic.Services;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Application.Services
{
    public interface IVacationBaseService
    {
        List<VacationBaseDTO> GetAllVacationBases();
        VacationBaseDTO GetVacationBaseById(Guid id);    
        ActionResult CreateVacationBase(VacationBaseDTO model);
    }

    public class VactionBaseService : IVacationBaseService
    {

        public ActionResult CreateVacationBase(VacationBaseDTO model)
        {
            var vacationBase = new VacationBase
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                AccommodationType = model.AccommodationType,
                PricePerNight = model.PricePerNight,
                Capacity = model.Capacity,
                IsAvailable = model.IsAvailable,
                ImageUrl = model.ImageUrl
            };
            
            using (var context = new TWebDbContext())
            {
                context.VacationBases.Add(vacationBase);
                context.SaveChanges();
            }

            return new RedirectResult("/Home/Index");
        }


        public List<VacationBaseDTO> GetAllVacationBases()
        {
            using (var context = new TWebDbContext())
            {
                return context.VacationBases
                    .Select(v => new VacationBaseDTO
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Description = v.Description,
                        AccommodationType = v.AccommodationType,
                        PricePerNight = v.PricePerNight,
                        Capacity = v.Capacity,
                        IsAvailable = v.IsAvailable,
                        ImageUrl = v.ImageUrl
                    })
                    .ToList();
            }
        }

        public VacationBaseDTO GetVacationBaseById(Guid id)
        {
            using (var context = new TWebDbContext())
            {
                var vacationBase = context.VacationBases
                    .FirstOrDefault(v => v.Id == id);

                if (vacationBase == null)
                    return null;

                return new VacationBaseDTO
                {
                    Id = vacationBase.Id,
                    Name = vacationBase.Name,
                    Description = vacationBase.Description,
                    AccommodationType = vacationBase.AccommodationType,
                    PricePerNight = vacationBase.PricePerNight,
                    Capacity = vacationBase.Capacity,
                    IsAvailable = vacationBase.IsAvailable,
                    ImageUrl = vacationBase.ImageUrl
                };
            }
        }

        // Updated method - filter by date and capacity only
        public List<VacationBaseDTO> FilterAccommodationOptions(DateTime checkInDate, DateTime checkOutDate, int capacity)
        {
            using (var context = new TWebDbContext())
            {
                var bookingService = new BookingService();

                var accommodationOptions = context.VacationBases
                    .Where(v => v.IsAvailable && v.Capacity >= capacity)
                    .ToList();

                var availableOptions = new List<VacationBaseDTO>();

                foreach (var option in accommodationOptions)
                {
                    if (bookingService.IsVacationBaseAvailable(option.Id, checkInDate, checkOutDate))
                    {
                        availableOptions.Add(new VacationBaseDTO
                        {
                            Id = option.Id,
                            Name = option.Name,
                            Description = option.Description,
                            AccommodationType = option.AccommodationType,
                            PricePerNight = option.PricePerNight,
                            Capacity = option.Capacity,
                            IsAvailable = true,
                            ImageUrl = option.ImageUrl
                        });
                    }
                }

                return availableOptions;
            }
        }        
    }
}
