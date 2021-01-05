using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Models.ClientVehicle
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
