using AutoShop.Web.Data;
using AutoShop.Web.Data.Entities;
using AutoShop.Web.Data.Repositories;
using AutoShop.Web.Helpers;
using AutoShop.Web.Models.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly DataContext _context;
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;

        public ClientsController(DataContext context, IClientRepository clientRepository, IUserHelper userHelper, IMailHelper mailHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _clientRepository = clientRepository;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
        }

        // GET: Clients
        [Authorize(Roles = "Employee,Admin")]
        public IActionResult Index()
        {
            var clients = _clientRepository.GetAllWithUser();

            return View(clients);
        }

        // GET: Clients/Details/5
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);

            if (client == null)
            {
                return NotFound();
            }

            client.User = await _userHelper.GetUserByIdAsync(client.UserId);

            if (client.User == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);

                if (user == null)
                {
                    // Creates a new Client
                    var client = new Client
                    {
                        NIB = model.NIB,
                        User = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Gender = model.Gender,
                            BirthDate = model.BirthDate,
                            Address = model.Address,
                            UserName = model.UserName,
                            Email = model.UserName
                        }
                    };

                    // Adds the User to the DataBase
                    var result = await _userHelper.AddUserAsync(client.User, model.Password);

                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The Client could not be created");
                        return View(model);
                    }

                    // Add user to Role
                    await _userHelper.AddUserToRoleAsync(client.User, "Client");

                    // Adds the Client to the DataBase
                    await _clientRepository.CreateAsync(client);

                    // Creates a Token in order to confirm the email
                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(client.User);

                    var success = await _userHelper.ConfirmEmailAsync(client.User, myToken);

                    if (success.Succeeded)
                    {
                        //Sends an Email to the User with the TokenLink
                        try
                        {
                            _mailHelper.SendMail(model.UserName, "Account Credentials", $"<h1>Account Credentials</h1>" +
                          $"To sign in for the first time, " +
                          $"plase use the following credentials:</br></br> Username = {model.UserName}</br></br> Password = {model.Password}");

                            ViewBag.Message = "The instructions to sign in to your account have been sent to email.";
                        }
                        catch (Exception e)
                        {
                            this.ModelState.AddModelError(string.Empty, e.Message);
                        }
                    }
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "A Client with this email is alredy registered");
            }

            return View(model);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);

            if (client == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(client.UserId);

            if(user == null)
            {
                return NotFound();
            }

            client.User = user;

            var model = _converterHelper.ToClientEditViewModel(client);

            return View(model);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="Admin, Employee")]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = await _clientRepository.GetByIdAsync(model.Id);

                var user = await _userHelper.GetUserByIdAsync(client.UserId);

                client = _converterHelper.ToClient(model, user, false);

                try
                {
                    await _clientRepository.UpdateAsync(client);
                    ViewBag.UserMessage = "Client updated";
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Update failed. Please try again later!");
                    return View(model);
                }
            }
            return View(model);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var client = await _clientRepository.GetByIdAsync(id.Value);

                if (client == null)
                {
                    return NotFound();
                }

                var user = await _userHelper.GetUserByIdAsync(client.UserId);

                await _clientRepository.DeleteAsync(client);
                return StatusCode(200, "Success");
            }
            catch (Exception)
            {
                return StatusCode(520, "Unknown Error.");
            }
        }
    }
}
