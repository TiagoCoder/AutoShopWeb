using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Orders
{
    public class IndexViewModel
    {
        [Display(Name = "Order Number")]
        public int Id { get; set; }

        [Display(Name = "Client Email")]
        public string ClientEmail { get; set; }

        [Display(Name = "Approved")]
        public bool IsApproved { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        [Display(Name = "Delivery Date")]
        public DateTime? DeliveryDate { get; set; }

        public string Registration { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        [Display(Name = "Services Applied")]
        public int ServicesQtd { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Final Price")]
        public decimal Price { get; set; }
    }
}
