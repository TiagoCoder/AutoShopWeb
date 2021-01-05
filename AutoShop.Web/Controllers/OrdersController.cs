using AutoShop.Web.Data.Entities;
using AutoShop.Web.Data.Repositories;
using AutoShop.Web.Helpers;
using AutoShop.Web.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IServices_OrdersRepository _services_OrdersRepository;
        private readonly IUserHelper _userHelper;
        private readonly IClientRepository _clientRepository;
        private readonly IClientVehicleRepository _clientVehicleRepository;
        private readonly IServiceRepository _serviceRepository;

        public OrdersController(IOrderRepository orderRepository, IServices_OrdersRepository services_OrdersRepository, IUserHelper userHelper, IClientRepository clientRepository, IClientVehicleRepository clientVehicleRepository, IServiceRepository serviceRepository)
        {
            _orderRepository = orderRepository;
            _services_OrdersRepository = services_OrdersRepository;
            _userHelper = userHelper;
            _clientRepository = clientRepository;
            _clientVehicleRepository = clientVehicleRepository;
            _serviceRepository = serviceRepository;
        }

        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Index()
        {
            var orders = _orderRepository.GetAllWithServices()
                .Where(o => o.IsAproved)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            var models = new List<IndexViewModel>();

            foreach (var order in orders)
            {
                var vehicle = _orderRepository.GetClientVehicle(order.ClientVehicleId, order.ClientId);

                var client = await _clientRepository.GetByIdWithUserAsync(order.ClientId);

                var model = new IndexViewModel
                {
                    Id = order.Id,
                    ClientEmail = client.User.Email,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate,
                    Registration = vehicle.Registration,
                    Brand = vehicle.VehicleInfo.Brand,
                    Model = vehicle.VehicleInfo.Model,
                    Year = vehicle.VehicleInfo.Year,
                    Price = order.Value,
                    ServicesQtd = order.Lines,
                    IsApproved = order.IsAproved
                };

                models.Add(model);
            }
            return View(models);
        }

        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> RequestIndex()
        {
            var orders = _orderRepository.GetAllWithServices()
                .Where(o => !o.IsAproved)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            var models = new List<IndexViewModel>();

            foreach (var order in orders)
            {
                var vehicle = _orderRepository.GetClientVehicle(order.ClientVehicleId, order.ClientId);

                var client = await _clientRepository.GetByIdWithUserAsync(order.ClientId);

                var model = new IndexViewModel
                {
                    Id = order.Id,
                    ClientEmail = client.User.Email,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate,
                    Registration = vehicle.Registration,
                    Brand = vehicle.VehicleInfo.Brand,
                    Model = vehicle.VehicleInfo.Model,
                    Year = vehicle.VehicleInfo.Year,
                    Price = order.Value,
                    ServicesQtd = order.Lines,
                    IsApproved = order.IsAproved
                };

                models.Add(model);
            }
            return View(models);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveOrder(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.GetByIdAsync(id.Value);

            if(order == null)
            {
                return NotFound();
            }
            else
            {
                var model = new ApproveOrderViewModel
                {
                    OrderId = id.Value
                };

                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveOrder(ApproveOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = await _orderRepository.GetByIdAsync(model.OrderId);

                order.IsAproved = true;
                order.DeliveryDate = model.DeliveryDate;

                try
                {
                    await _orderRepository.UpdateAsync(order);
                    ViewBag.Message = "Order approved with success!";
                    return RedirectToAction("RequestIndex");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Order approval failed! Please ensure the delivery date is valid");
                    return View(model);
                }
            }

            ModelState.AddModelError("", "Order approval failed! Please ensure the delivery date is valid");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ClientIndex()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            var client = _clientRepository.GetAll().FirstOrDefault(c => c.UserId == user.Id);

            if (client == null)
            {
                return NotFound();
            }

            var orders = _orderRepository.GetAllWithServices()
                .Where(o => o.ClientId == client.Id && o.IsAproved)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            var models = new List<IndexViewModel>();

            foreach (var order in orders)
            {
                var vehicle = _orderRepository.GetClientVehicle(order.ClientVehicleId, order.ClientId);

                var model = new IndexViewModel
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate,
                    Registration = vehicle.Registration,
                    Brand = vehicle.VehicleInfo.Brand,
                    Model = vehicle.VehicleInfo.Model,
                    Year = vehicle.VehicleInfo.Year,
                    Price = order.Value,
                    ServicesQtd = order.Lines,
                    IsApproved = order.IsAproved
                };

                models.Add(model);
            }
            return View(models);
        }

        [Authorize]
        public async Task<IActionResult> ClientRequestIndex()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            var client = _clientRepository.GetAll().FirstOrDefault(c => c.UserId == user.Id);

            if (client == null)
            {
                return NotFound();
            }

            var orders = _orderRepository.GetAllWithServices()
                .Where(o => o.ClientId == client.Id && !o.IsAproved)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            var models = new List<IndexViewModel>();

            foreach (var order in orders)
            {
                var vehicle = _orderRepository.GetClientVehicle(order.ClientVehicleId, order.ClientId);

                var model = new IndexViewModel
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate,
                    Registration = vehicle.Registration,
                    Brand = vehicle.VehicleInfo.Brand,
                    Model = vehicle.VehicleInfo.Model,
                    Year = vehicle.VehicleInfo.Year,
                    Price = order.Value,
                    ServicesQtd = order.Lines,
                    IsApproved = order.IsAproved
                };

                models.Add(model);
            }
            return View(models);
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> NonApprovedIndex()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            var client = _clientRepository.GetAll().FirstOrDefault(c => c.UserId == user.Id);

            if (client == null)
            {
                return NotFound();
            }

            var orders = _orderRepository.GetAllWithServices().Where(o => o.ClientId == client.Id && !o.IsAproved).ToList();

            var models = new List<IndexViewModel>();

            foreach (var order in orders)
            {
                var vehicle = _orderRepository.GetClientVehicle(order.ClientVehicleId, order.ClientId);

                var model = new IndexViewModel
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate,
                    Registration = vehicle.Registration,
                    Brand = vehicle.VehicleInfo.Brand,
                    Model = vehicle.VehicleInfo.Model,
                    Year = vehicle.VehicleInfo.Year,
                    Price = order.Value,
                    ServicesQtd = order.Lines,
                    IsApproved = order.IsAproved
                };

                models.Add(model);
            }
            return View(models);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            var client = _clientRepository.GetAll().Where(c => c.UserId == user.Id).AsNoTracking().FirstOrDefault();

            if (client == null)
            {
                return NotFound();
            }

            var vehicles = await _clientVehicleRepository.GetAllVehiclesByClient(client.Id);

            ViewBag.Data = vehicles;

            var services = _serviceRepository.GetAll();

            var servicesModel = new List<SelectedServicesViewModel>();

            foreach(var service in services)
            {
                var serviceModel = new SelectedServicesViewModel
                {
                    Service = service
                };

                servicesModel.Add(serviceModel);
            }

            var model = new CreateViewModel
            {
                Services = servicesModel,
                OrderDate = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            var thisClient = _clientRepository.GetAllWithUser().Where(c => c.User.Email == this.User.Identity.Name).AsNoTracking().FirstOrDefault();

            var vehicles = await _clientVehicleRepository.GetAllVehiclesByClient(thisClient.Id);

            ViewBag.Data = vehicles;

            if (ModelState.IsValid)
            {

                if (model.OrderDate <= DateTime.Now)
                {
                    ModelState.AddModelError("", "There was an error creating the order. Please select a valid date!");
                    return View(model);
                }

                foreach(var item in model.Services)
                {
                    if(item.Selected)
                    {
                        if (item.Qtd == 0)
                        {
                            ModelState.AddModelError("", "There was an error creating the order. Please select a valid quantity!");
                            return View(model);
                        }
                    }
                }

                var clientVehicle = await _clientVehicleRepository.GetVehicleByRegistration(model.Registration);

                if (clientVehicle == null)
                {
                    ModelState.AddModelError("", "The selected vehicle does not exist! Please check your vehicles as data may have been updated!");
                    return View(model);
                }

                var client = _clientRepository.GetAllWithUser().Where(c => c.User.Email == this.User.Identity.Name).AsNoTracking().FirstOrDefault();

                if (client == null)
                {
                    return NotFound();
                }

                var order = new Order
                {
                    IsAproved = false,
                    ClientVehicleId = clientVehicle.Id,
                    OrderDate = model.OrderDate,
                    ClientId = client.Id
                };

                try
                {
                   await _orderRepository.CreateAsync(order);

                    var createdOrder = _orderRepository.GetAll().Where(o => o.OrderDate == model.OrderDate && o.ClientId == client.Id).AsNoTracking().FirstOrDefault();

                    foreach (var selectedService in model.Services)
                    {
                        if (selectedService.Selected)
                        {
                            var serviceOrder = new Services_Orders
                            {
                                ServiceId = selectedService.Service.Id,
                                Qtd = selectedService.Qtd,
                                OrderId = createdOrder.Id
                            };

                            await _services_OrdersRepository.CreateAsync(serviceOrder);
                        }
                    }

                    ViewBag.Message = "Order requested with success! Please await for aproval.";
                    RedirectToAction("ClientIndex");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "There was an error creating the order. Please try again later!");
                    return View(model);
                }
            }

            ModelState.AddModelError("", "There was an error creating the order. Please check that all information is valid!");
            return View(model);
        }

        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var services = _orderRepository.GetOrderServices(id.Value);

            if (services == null)
            {
                return NotFound();
            }

            var models = new List<DetailsViewModel>();

            foreach (var service in services)
            {
                var model = new DetailsViewModel
                {
                    Description = service.Service.Description,
                    Price = service.Service.Price,
                    Qtd = service.Qtd,
                    TotalPrice = service.Service.Price * service.Qtd
                };

                models.Add(model);
            }

            return View(models);
        }
    }
}
