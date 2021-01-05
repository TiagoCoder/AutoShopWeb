using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Role
{
    public class CreateViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string Role { get; set; }
    }
}
