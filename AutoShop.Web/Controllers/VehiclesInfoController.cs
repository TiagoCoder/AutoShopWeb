using AutoShop.Web.Data.Entities;
using AutoShop.Web.Data.Repositories;
using AutoShop.Web.Models.VehiclesInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    public class VehiclesInfoController : Controller
    {
        private readonly IVehiclesInfoRepository _vehiclesInfoRepository;

        public VehiclesInfoController(IVehiclesInfoRepository vehiclesInfoRepository)
        {
            _vehiclesInfoRepository = vehiclesInfoRepository;
        }

        public IActionResult Index()
        {
            return View(_vehiclesInfoRepository.GetAll().OrderBy(v => v.Brand));
        }

        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var exists = _vehiclesInfoRepository.GetAll()
                    .Where(v => v.Brand == model.Brand && v.Model == model.CarModel && v.Year == model.Year.ToString())
                    .FirstOrDefault();

                if (exists == null)
                {
                    try
                    {
                        var vehicleInfo = new VehicleInfo
                        {
                            Brand = model.Brand,
                            Model = model.CarModel,
                            Year = model.Year.ToString()
                        };

                        await _vehiclesInfoRepository.CreateAsync(vehicleInfo);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "A DataBase error occured while creating a new Vehicle! Please contact an Administrator as soon as possible.");
                        return View(model);
                    }
                }

                ModelState.AddModelError("", "A Vehicle with this information alredy exists!");
                return View(model);
            }

            return View(model);
        }
    }
}
