using AutoShop.Web.Data.Entities;
using AutoShop.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoShop.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);
        Task CheckRoleAsync(string roleName);
        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task AddUserToRoleAsync(User user, string roleName);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<User> GetUserByIdAsync(string userID);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        List<string> GetRoles();
        IEnumerable<SelectListItem> GetRolesAsSelect();

        Task<IEnumerable<Models.Role.IndexViewModel>> GetRolesWithUsersCount();

        Task<string> GetUserMainRoleAsync(User user);
    }
}
