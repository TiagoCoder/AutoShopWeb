using AutoShop.Web.Data.Entities;
using AutoShop.Web.Helpers;
using AutoShop.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;

        public AccountController(IUserHelper userHelper, IConfiguration configuration, IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
        }

        public IActionResult ClientLogin()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ClientLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                if(user != null)
                {
                    var result = await _userHelper.LoginAsync(model);

                    if (result.Succeeded)
                    {
                        if (Request.Query.Keys.Contains("ReturnUrl"))
                        {
                            return Redirect(this.Request.Query["ReturnUrl"].First());
                        }

                        return this.RedirectToAction("Index", "Home");
                    }
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to Login");
            return this.View(model);
        }

        public IActionResult EmployeeLogin()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                if (user != null)
                {
                    var test = await _userHelper.IsUserInRoleAsync(user, "Client");

                    if (test)
                    {
                        return this.RedirectToAction("NotAuthorized", "Account");
                    }

                    var result = await _userHelper.LoginAsync(model);

                    if (result.Succeeded)
                    {
                        if (Request.Query.Keys.Contains("ReturnUrl"))
                        {
                            return Redirect(this.Request.Query["ReturnUrl"].First());
                        }

                        return this.RedirectToAction("Index", "Home");
                    }
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to Login");
            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.UserName,
                        UserName = model.UserName
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The User could not be created");
                        return View(model);
                    }

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken,
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.UserName, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                       $"To confirm your account, " +
                       $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                    this.ViewBag.Message = "The instructions to allow your user have been sent to email.";

                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "The User is alredy registered");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var model = new EditViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    var response = await _userHelper.UpdateUserAsync(user);

                    if (response.Succeeded)
                    {
                        ViewBag.UserMessage = "User updated";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                }
            }

            return View(model);
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        ViewBag.Message = "Password Updated with success!!";
                        return View();
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return View(model);
        }


        public IActionResult RecoverPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return this.View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(model.Email, "Password Reset", $"<h1>Password Reset</h1>" +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");

                this.ViewBag.Message = "The instructions to recover your password has been sent to email.";
                return this.View();

            }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            // Verifies if the userId and token are not empty
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            // Gets the User through its Id
            var user = await _userHelper.GetUserByIdAsync(userId);

            if (user == null)
            {
                return this.NotFound();
            }

            // Initializes a new Model, filling the fields 
            var model = new ResetPasswordViewModel
            {
                UserName = user.UserName,
                Token = token
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successful.";
                    return this.View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
