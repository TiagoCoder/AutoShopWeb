using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.EmployeeModels
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public char Gender { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public string NIB { get; set; }

        public string Role { get; set; }
    }
}
