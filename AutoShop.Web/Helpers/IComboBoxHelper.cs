using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AutoShop.Web.Helpers
{
    public interface IComboBoxHelper
    {
        IEnumerable<SelectListItem> GetBrands();

        IEnumerable<SelectListItem> GetModels(string Brand);

        IEnumerable<SelectListItem> GetYears(string Model);

        Models.ClientVehicle.CreateViewModel ReloadComboBoxes(Models.ClientVehicle.CreateViewModel model);
    }
}
