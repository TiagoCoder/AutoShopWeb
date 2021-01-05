using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
