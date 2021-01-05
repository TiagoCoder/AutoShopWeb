using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Account
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
