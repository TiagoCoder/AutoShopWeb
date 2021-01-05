using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.EmployeeModels
{
    public class CreateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Prompt = "Email...")]
        [EmailAddress]
        public string Email { get; set; }

        public string Username => Email;

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

    }
}
