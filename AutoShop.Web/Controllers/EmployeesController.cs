using AutoShop.Web.Data;
using AutoShop.Web.Data.Entities;
using AutoShop.Web.Data.Repositories;
using AutoShop.Web.Helpers;
using AutoShop.Web.Models.EmployeeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DataContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;

        public EmployeesController(DataContext context, IEmployeeRepository employeeRepository, IUserHelper userHelper, IMailHelper mailHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
        }

        // GET: Employees
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var employees = _employeeRepository.GetAllWithUser().ToList();

            var model =  await _converterHelper.ToEmployeeIndexViewModel(employees);

            return View(model);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetByIdAsync(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(employee.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToEmployeeEditViewModel(employee, user);

            return View(model);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new CreateViewModel
            {
                Roles = _userHelper.GetRolesAsSelect(),
                BirthDate = DateTime.Now
            };

            return View(model);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);

                if (user == null)
                {
                    var employee = new Employee
                    {
                        User = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            BirthDate = model.BirthDate,
                            UserName = model.Username,
                            Address = model.Address,
                        }
                    };

                    // Adds the User to the DataBase
                    var result = await _userHelper.AddUserAsync(employee.User, model.Password);

                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The User could not be created");
                        return View(model);
                    }
                    else
                    {
                        // Add user to Role
                        await _userHelper.AddUserToRoleAsync(employee.User, "Employee");
                    }

                    try
                    {
                        await _employeeRepository.CreateAsync(employee);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "The employee could not be created");
                        return View(model);
                    }

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(employee.User);

                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = employee.User.Id,
                        token = myToken,
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                       $"To allow the user, " +
                       $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                    this.ViewBag.Message = "The instructions to allow your account has been sent to email.";

                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "An Employee with this email is alredy registered");
            }
            // Seed the Roles again
            model.Roles = _userHelper.GetRolesAsSelect();

            return View(model);
        }

        // GET: Employees/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetByIdAsync(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(employee.UserId);

            var model = _converterHelper.ToEmployeeEditViewModel(employee, user);

            return View(model);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = await _employeeRepository.GetByIdAsync(model.Id);

                var user = await _userHelper.GetUserByIdAsync(employee.UserId);

                employee = _converterHelper.ToEmployee(model, user, false);

                try
                {
                    await _employeeRepository.UpdateAsync(employee);
                    ViewBag.UserMessage = "Employee Updated!";
                }
                catch (Exception)
                {
                    ViewBag.UserMessage = "Error updating Information. Please try again later!";
                }

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id.Value);

                if (employee == null)
                {
                    return NotFound();
                }

                var user = await _userHelper.GetUserByIdAsync(employee.UserId);

                await _employeeRepository.DeleteAsync(employee);
                return StatusCode(200, "Success");
            }
            catch (Exception)
            {
                return StatusCode(520, "Unknown Error.");
            } 
        }
    }
}
