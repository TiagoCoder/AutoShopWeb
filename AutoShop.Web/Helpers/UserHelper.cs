using AutoShop.Web.Data;
using AutoShop.Web.Data.Entities;
using AutoShop.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }


        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public List<string> GetRoles()
        {
            return _roleManager.Roles.Select(r => r.Name).AsNoTracking().ToList();
        }

        public IEnumerable<SelectListItem> GetRolesAsSelect()
        {
            var list = _roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Please Choose a Role...",
                Value = ""
            });

            return list;
        }

        public async Task<IEnumerable<Models.Role.IndexViewModel>> GetRolesWithUsersCount()
        {
            var list = _roleManager.Roles.Select(r => new Models.Role.IndexViewModel
            {
                Role = r.Name
            }).ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                list[i].NumberOfUsers = (await _userManager.GetUsersInRoleAsync(list[i].Role)).Count();
            }

            return list;
        }

        public async Task<string> GetUserMainRoleAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault(x => x == "Admin" || x == "Employee");
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName,
                });
            }
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(string userID)
        {
            return await _userManager.FindByIdAsync(userID);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
            model.Username,
            model.Password,
            model.RememberMe,
            false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }



    }
}
