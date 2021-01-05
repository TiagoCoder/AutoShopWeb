using AutoShop.Web.Data.Entities;

namespace AutoShop.Web.Data.Repositories
{
    public class ServicesOrdersRepository : GenericRepository<Services_Orders>, IServices_OrdersRepository
    {
        private readonly DataContext _context;

        public ServicesOrdersRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
