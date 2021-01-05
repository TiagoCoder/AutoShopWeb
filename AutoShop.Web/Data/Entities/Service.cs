using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Entities
{
    public class Service : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
