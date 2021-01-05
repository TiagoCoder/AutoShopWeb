using AutoShop.Web.Data;
using AutoShop.Web.Data.Entities;
using AutoShop.Web.Data.Repositories;
using AutoShop.Web.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly IClientRepository _clientRepository;
        private readonly IComboBoxHelper _comboBoxHelper;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;

        public ConverterHelper(IClientRepository clientRepository, IComboBoxHelper comboBoxHelper, IUserHelper userHelper, DataContext context)
        {
            _clientRepository = clientRepository;
            _comboBoxHelper = comboBoxHelper;
            _userHelper = userHelper;
            _context = context;
        }

        public Client ToClient(CreateViewModel model, User user)
        {
            return new Client
            {
                NIB = model.NIB,
                User = user
            };
        }

        public Client ToClient(EditViewModel model, User user, bool isNew)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.BirthDate = model.BirthDate;
            user.Address = model.Address;

            return new Client
            {
                Id = isNew ? 0 : model.Id,
                NIB = model.NIB,
                User = user
            };
        }

        public Employee ToEmployee(Models.EmployeeModels.EditViewModel model, User user, bool isNew)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.BirthDate = model.BirthDate;
            user.Address = model.Address;

            return new Employee
            {
                Id = isNew ? 0 : model.Id,
                NIB = model.NIB,
                User = user
            };
        }

        public EditViewModel ToClientEditViewModel(Client client)
        {
            return new EditViewModel
            {
                Id = client.Id,
                NIB = client.NIB,
                FirstName = client.User.FirstName,
                LastName = client.User.LastName,
                Gender = client.User.Gender,
                BirthDate = client.User.BirthDate,
                Address = client.User.Address
            };
        }

        public async Task<IEnumerable<Models.EmployeeModels.IndexViewModel>> ToEmployeeIndexViewModel(List<Employee> employees)
        {
            var list = new List<Models.EmployeeModels.IndexViewModel>();

            foreach(var employee in employees)
            {
                var model = new Models.EmployeeModels.IndexViewModel
                {
                    Id = employee.Id,
                    FirstName = employee.User.FirstName,
                    LastName = employee.User.LastName,
                    NIB = employee.NIB,
                    Role = await _userHelper.GetUserMainRoleAsync(employee.User)
                };

                list.Add(model);
            }

            return list;
        }

        public Models.EmployeeModels.EditViewModel ToEmployeeEditViewModel(Employee employee, User user)
        {
            return new Models.EmployeeModels.EditViewModel
            {
                Id = employee.Id,
                NIB = employee.NIB,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                BirthDate = user.BirthDate,
                Gender = user.Gender
            };
        }

        public async Task<ClientVehicle> ToClientVehicle(Models.ClientVehicle.CreateViewModel model)
        {
            var vehicle = new VehicleInfo
            {
                Brand = _context.VehicleInfo.FirstOrDefault(v => v.Id == model.SelectedBrand).Brand,
                Model = _context.VehicleInfo.FirstOrDefault(v => v.Id == model.SelectedModel).Model,
                Year = _context.VehicleInfo.FirstOrDefault(v => v.Id == model.SelectedYear).Year
            };

            var result = _context.VehicleInfo.Where(v => v.Brand == vehicle.Brand && v.Model == vehicle.Model && v.Year == vehicle.Year).FirstOrDefault();

            return new ClientVehicle
            {
                Client = await _clientRepository.GetByIdAsync(model.ClientId),
                Registration = model.Registration,
                Color = model.Color,
                VehicleInfo = result
            };
        }

        public VehicleInfo ToVehicleInfo(Models.VehiclesInfo.EditViewModel model)
        {
            var vehicle = new VehicleInfo
            {
                Brand =  _context.VehicleInfo.FirstOrDefault(v => v.Id == model.SelectedBrand).Brand,
                Model = _context.VehicleInfo.FirstOrDefault(v => v.Id == model.SelectedModel).Model,
                Year = _context.VehicleInfo.FirstOrDefault(v => v.Id == model.SelectedYear).Year
            };

            return _context.VehicleInfo.Where(v => v.Brand == vehicle.Brand && v.Model == vehicle.Model && v.Year == vehicle.Year).FirstOrDefault();
        }
    }
}
