using AutoShop.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.Orders
{
    public class SelectedServicesViewModel
    {
        public bool Selected { get; set; }

        [Required]
        public Service Service { get; set; } 

        public int Qtd { get; set; }


    }
}
