using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Orders
{
    public class DetailsViewModel
    {
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Display(Name="Quantity")]
        public int Qtd { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }
}
