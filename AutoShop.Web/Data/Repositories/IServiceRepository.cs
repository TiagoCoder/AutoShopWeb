using AutoShop.Web.Data.Entities;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Repositories
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<bool> DeleteServiceAsync(int id);
    }
}
