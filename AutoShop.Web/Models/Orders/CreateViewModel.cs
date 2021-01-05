using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Orders
{
    public class CreateViewModel
    {
        [Required]
        public string Registration { get; set; }

        [Required]
        public List<SelectedServicesViewModel> Services { get; set; }

        [Display(Name = "Order date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
    }
}
