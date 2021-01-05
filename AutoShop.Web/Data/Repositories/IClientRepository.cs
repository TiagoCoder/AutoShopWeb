using AutoShop.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Repositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        IQueryable<Client> GetAllWithUser();

        Task<bool> DeleteClientAsync(Client client);
        Task<Client> GetByIdWithUserAsync(int clientId);

        Task<Client> GetByEmailAsync(string email);
    }
}
