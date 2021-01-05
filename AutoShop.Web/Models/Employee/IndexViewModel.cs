using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.EmployeeModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string NIB { get; set; }

        public string Role { get; set; }
    }
}
