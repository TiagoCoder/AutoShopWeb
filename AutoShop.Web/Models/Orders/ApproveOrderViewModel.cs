using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Orders
{
    public class ApproveOrderViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }
    }
}
