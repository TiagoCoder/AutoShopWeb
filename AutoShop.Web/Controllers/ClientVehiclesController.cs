using AutoShop.Web.Data;
using AutoShop.Web.Data.Entities;
using AutoShop.Web.Data.Repositories;
using AutoShop.Web.Helpers;
using AutoShop.Web.Models.ClientVehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    public class ClientVehiclesController : Controller
    {
        private readonly DataContext _context;
        private readonly IClientVehicleRepository _clientVehicleRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IComboBoxHelper _comboBoxHelper;
        private readonly IClientRepository _clientRepository;

        public ClientVehiclesController(DataContext context, IClientVehicleRepository clientVehicleRepository, IConverterHelper converterHelper, IComboBoxHelper comboBoxHelper, IClientRepository clientRepository)
        {
            _context = context;
            _clientVehicleRepository = clientVehicleRepository;
            _converterHelper = converterHelper;
            _comboBoxHelper = comboBoxHelper;
            _clientRepository = clientRepository;
        }

        // GET: Vehicles/5
        [Authorize]
        public async Task<IActionResult> Index(int? id)
        {
            if (User.IsInRole("Client"))
            {

                var client = await _clientRepository.GetByEmailAsync(this.User.Identity.Name);

                if(client == null)
                {
                    return NotFound();
                }

                var vehicles = await _clientVehicleRepository.GetAllVehiclesByClient(client.Id);

                return View(vehicles);
            }
            else
            {
                var vehicles = await _clientVehicleRepository.GetAllVehiclesByClient(id.Value);

                return View(vehicles);
            }
        }

        // GET: Vehicles/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _clientVehicleRepository.GetVehicleById(id.Value);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        [Authorize]
        public IActionResult Create()
        {
            var Brands = _comboBoxHelper.GetBrands();

            var model = new CreateViewModel
            {
                Brands = Brands
            };

            return View(model);
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            model = _comboBoxHelper.ReloadComboBoxes(model);

            if (ModelState.IsValid)
            {
                var vehicle = await _clientVehicleRepository.GetVehicleByRegistration(model.Registration);

                if(vehicle == null)
                {
                    var client = await _clientRepository.GetByEmailAsync(this.User.Identity.Name);

                    if(client == null)
                    {
                        return NotFound();
                    }

                    var clientVehicle = new ClientVehicle
                    {
                        ClientId = client.Id,
                        Registration = model.Registration,
                        Color = model.Color,
                        VehicleInfoId = _clientVehicleRepository.GetVehicleInfo(model.SelectedBrand, model.SelectedModel, model.SelectedYear)
                    };

                    try
                    {
                        await _clientVehicleRepository.CreateAsync(clientVehicle);
                        ViewBag.Message = "Vehicle created with success!";
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "The vehicle could not be created");

                        model = _comboBoxHelper.ReloadComboBoxes(model);

                        return View(model);
                    }
                }

                return View(model);
            }

            return View(model);
        }

        // GET: Vehicles/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _clientVehicleRepository.GetVehicleById(id.Value);

            if (vehicle == null)
            {
                return NotFound();
            }

            var model = new EditViewModel
            {
                Id = vehicle.Id,
                Color = vehicle.Color
            };

            return View(model);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vehicle = await _clientVehicleRepository.GetByIdAsync(model.Id);

                if(vehicle == null)
                {
                    return NotFound();
                }

                vehicle.Color = model.Color;

                try
                {
                    await _clientVehicleRepository.UpdateAsync(vehicle);
                    ViewBag.Message = "Vehicle updated with success!";
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "The vehicle could not be updated");

                    return View(model);
                } 
            }

            return View(model);
        }

        // GET: Vehicles/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.ClientVehicles
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.ClientVehicles.FindAsync(id);
            _context.ClientVehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult GetModelsAsync(string selectedBrand)
        {
            var models = _comboBoxHelper.GetModels(selectedBrand);
            return this.Json(models);
        }

        public JsonResult GetModelYearsAsync(string selectedModel)
        {
            var modelYears = _comboBoxHelper.GetYears(selectedModel);
            return this.Json(modelYears);
        }
    }
}
