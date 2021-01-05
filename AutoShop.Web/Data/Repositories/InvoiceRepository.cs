using AutoShop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly DataContext _context;

        public InvoiceRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
