using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Role
{
    public class IndexViewModel
    {
        [Required]

        [Display(Name = "Role Name")]
        public string Role { get; set; }

        [Display(Name = "Quantity of Users")]
        public int NumberOfUsers { get; set; }
    }
}
