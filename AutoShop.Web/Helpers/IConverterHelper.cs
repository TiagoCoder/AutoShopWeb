using AutoShop.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoShop.Web.Helpers
{
    public interface IConverterHelper
    {
        Client ToClient(Models.Client.CreateViewModel model, User user);

        Client ToClient(Models.Client.EditViewModel model, User user, bool isNew);

        Employee ToEmployee(Models.EmployeeModels.EditViewModel model, User user, bool isNew);

        Models.Client.EditViewModel ToClientEditViewModel(Client client);

        Task<IEnumerable<Models.EmployeeModels.IndexViewModel>> ToEmployeeIndexViewModel(List<Employee> employees);

        Models.EmployeeModels.EditViewModel ToEmployeeEditViewModel(Employee employee, User user);

        Task<ClientVehicle> ToClientVehicle(Models.ClientVehicle.CreateViewModel model);

        VehicleInfo ToVehicleInfo(Models.VehiclesInfo.EditViewModel model);
    }
}
