using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Client
{
    public class EditViewModel
    {
        [Required]
        public int Id { get; set; }

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

        [Required]
        [StringLength(9)]
        public string NIB { get; set; }
    }
}
