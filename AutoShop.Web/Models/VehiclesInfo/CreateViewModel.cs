using System.ComponentModel.DataAnnotations;
using AutoShop.Web.CustomAttributes;

namespace AutoShop.Web.Models.VehiclesInfo
{
    public class CreateViewModel
    {
        [Required]
        public string Brand { get; set; }

        [Required]
        [Display(Name="Model")]
        public string CarModel { get; set; }

        [Required]
        [RangeUntilYearAttribute(1900)]
        public int Year { get; set; }
    }
}
