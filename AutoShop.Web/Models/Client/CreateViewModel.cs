using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Client
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
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(9)]
        public string NIB { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
