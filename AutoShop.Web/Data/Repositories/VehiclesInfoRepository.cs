using AutoShop.Web.Data.Entities;
using System.Collections.Generic;

namespace AutoShop.Web.Data.Repositories
{
    public class VehiclesInfoRepository : GenericRepository<VehicleInfo>, IVehiclesInfoRepository
    {
        private readonly DataContext _context;

        public VehiclesInfoRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
