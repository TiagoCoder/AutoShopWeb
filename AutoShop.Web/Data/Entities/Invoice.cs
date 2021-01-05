using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Entities
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }

        public Order Order { get; set; }
    }
}
