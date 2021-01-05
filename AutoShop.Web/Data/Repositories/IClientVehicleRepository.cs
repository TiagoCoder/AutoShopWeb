using AutoShop.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Repositories
{
    public interface IClientVehicleRepository : IGenericRepository<ClientVehicle>
    {
        IQueryable<ClientVehicle> GetAllWithInfo();
        Task<ICollection<ClientVehicle>> GetAllVehiclesByClient(int id);

        Task<ClientVehicle> GetVehicleById(int id);

        Task<ClientVehicle> GetVehicleByRegistration(string registration);

        int GetVehicleInfo(int BrandId, int ModelId, int YearId);
    }
}
