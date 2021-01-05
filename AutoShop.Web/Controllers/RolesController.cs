using AutoShop.Web.Helpers;
using AutoShop.Web.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IUserHelper _userHelper;

        public RolesController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Index()
        {
            var list = await _userHelper.GetRolesWithUsersCount();

            return View(list);
        }
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userHelper.CheckRoleAsync(model.Role);

                var roles = _userHelper.GetRoles();

                if (roles != null && roles.Contains(model.Role))
                {
                    this.ViewBag.Message = "Role created with success!";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Role alredy exists!");
                }
            }

            return View(model);
        }
    }
}
