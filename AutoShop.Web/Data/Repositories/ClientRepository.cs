using AutoShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Client> GetAllWithUser()
        {
            return _context.Clients.Include(e => e.User);
        }

        public async Task<bool> DeleteClientAsync(Client client)
        {
            _context.Clients.Remove(client);
            _context.Users.Remove(client.User);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Client> GetByIdWithUserAsync(int clientId)
        {
            return  await _context.Clients.Where(c => c.Id == clientId).Include(c => c.User).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Client> GetByEmailAsync(string email)
        {
            return await _context.Clients.Include(c => c.User).Where(c => c.User.Email == email).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
