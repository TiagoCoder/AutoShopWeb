using AutoShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly DataContext _context;

        public ServiceRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);

            if (service != null)
            {
                _context.Services.Remove(service);

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
            return false;
        }

    }
}
