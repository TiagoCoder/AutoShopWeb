using AutoShop.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Account
{
    public class RegisterViewModel : User
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public override string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
