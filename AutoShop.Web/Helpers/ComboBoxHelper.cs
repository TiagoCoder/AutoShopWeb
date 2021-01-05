using AutoShop.Web.Data;
using AutoShop.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Helpers
{
    public class ComboBoxHelper : IComboBoxHelper
    {
        private readonly DataContext _context;

        public ComboBoxHelper(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetBrands()
        {
            var list = _context.VehicleInfo.Select(v => new SelectListItem
            {
                Text = v.Brand,
                Value = v.Id.ToString()
            }).GroupBy(v => v.Text).Select(y => y.First()).OrderBy(o => o.Text).ToList();

            list.Add(new SelectListItem
            {
                Text = "Please select a Brand...",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetModels(string Brand)
        {
            var list = _context.VehicleInfo.Where(v => v.Brand == Brand).Select(v => new SelectListItem
            {
                Text = v.Model,
                Value = v.Id.ToString()
            }).GroupBy(v => v.Text).Select(y => y.First()).OrderBy(o => o.Text).ToList();

            return list;
        }

        public IEnumerable<SelectListItem> GetYears(string Model)
        {
            var list = _context.VehicleInfo.Where(v => v.Model == Model).Select(v => new SelectListItem
            {
                Text = v.Year,
                Value = v.Id.ToString()
            }).GroupBy(v => v.Text).Select(y => y.First()).OrderBy(o => o.Text).ToList();

            return list;
        }

        public Models.VehiclesInfo.EditViewModel ReloadComboBoxes(Models.VehiclesInfo.EditViewModel model)
        {
            model.Brands = GetBrands();

            var selectedBrand = model.Brands.Where(b => b.Value == model.SelectedBrand.ToString()).First();

            model.Models = GetModels(selectedBrand.Text);

            var selectedModel = model.Models.Where(m => m.Value == model.SelectedModel.ToString()).First();

            model.Years = GetYears(selectedModel.Text);

            return model;
        }

        public Models.ClientVehicle.CreateViewModel ReloadComboBoxes(Models.ClientVehicle.CreateViewModel model)
        {
            model.Brands = GetBrands();

            var selectedBrand = model.Brands.Where(b => b.Value == model.SelectedBrand.ToString()).First();

            model.Models = GetModels(selectedBrand.Text);

            var selectedModel = model.Models.Where(m => m.Value == model.SelectedModel.ToString()).First();

            model.Years = GetYears(selectedModel.Text);

            return model;
        }
    }
}
