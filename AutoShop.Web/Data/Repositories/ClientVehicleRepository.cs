using AutoShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Repositories
{
    public class ClientVehicleRepository : GenericRepository<ClientVehicle>, IClientVehicleRepository
    {
        private readonly DataContext _context;

        public ClientVehicleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ClientVehicle> GetAllWithInfo()
        {
            return _context.ClientVehicles.Include(c => c.VehicleInfo).AsNoTracking();
        }

        public async Task<ClientVehicle> GetVehicleById(int id)
        {
            return await _context.ClientVehicles.Where(c => c.Id == id).Include(t => t.Client.Vehicles).ThenInclude(i => i.VehicleInfo).FirstOrDefaultAsync();
        }

        public async Task<ClientVehicle> GetVehicleByRegistration(string registration)
        {
            return await _context.ClientVehicles.Where(c => c.Registration == registration).Include(i => i.VehicleInfo).FirstOrDefaultAsync();
        }

        public async Task<ICollection<ClientVehicle>> GetAllVehiclesByClient(int id)
        {
            return await _context.ClientVehicles.Where(c => c.Client.Id == id).Include(t => t.VehicleInfo).ToListAsync();
        }

        public int GetVehicleInfo(int BrandId, int ModelId, int YearId)
        {
            //string Brand = _context.VehicleInfo.Where(v => v.Id == BrandId).FirstOrDefault().Brand;

            //string Models = _context.VehicleInfo.Where(v => v.Id == ModelId).FirstOrDefault().Brand;

            return _context.VehicleInfo.Where(v => v.Id == BrandId && v.Id == ModelId && v.Id == YearId).FirstOrDefault().Id;
        }
    }
}
