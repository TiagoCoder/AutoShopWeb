using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.VehiclesInfo
{
    public class EditViewModel
    {
        [Required]
        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Brand")]
        public int SelectedBrand { get; set; }

        [Required]
        [Display(Name = "Model")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Model")]
        public int SelectedModel { get; set; }

        [Required]
        [Display(Name = "Model Year")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Model Year")]
        public int SelectedYear { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }

        public IEnumerable<SelectListItem> Models { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; }
    }
}
