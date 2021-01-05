using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.ClientVehicle
{
    public class CreateViewModel
    {
        public int ClientId { get; set; }

        [Required]
        public string Registration { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Brand")]
        public int SelectedBrand { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }

        public IEnumerable<SelectListItem> Models { get; set; }

        [Required]
        [Display(Name = "Model")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Model")]
        public int SelectedModel { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; }

        [Required]
        [Display(Name = "Model Year")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Model Year")]
        public int SelectedYear { get; set; }
    }
}
