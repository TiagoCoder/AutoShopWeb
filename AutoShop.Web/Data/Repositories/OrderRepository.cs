using AutoShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoShop.Web.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Order> GetAllWithServices()
        {
            return _context.Orders.Include(o => o.Items).ThenInclude(s => s.Service);
        }

        public ClientVehicle GetClientVehicle(int vehicleId, int clientId)
        {
            return _context.ClientVehicles.Where(v => v.Id == vehicleId && v.ClientId == clientId).Include(i => i.VehicleInfo).AsNoTracking().FirstOrDefault();
        }

         public IQueryable<Services_Orders> GetOrderServices(int Id)
        {
            return _context.Services_Orders.Where(o => o.OrderId == Id).Include(s => s.Service);
        }
    }
}
