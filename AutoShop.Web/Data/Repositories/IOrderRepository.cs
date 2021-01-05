using AutoShop.Web.Data.Entities;
using System.Linq;

namespace AutoShop.Web.Data.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        IQueryable<Order> GetAllWithServices();
        ClientVehicle GetClientVehicle(int vehicleId, int clientId);

        IQueryable<Services_Orders> GetOrderServices(int Id);
    }
}
