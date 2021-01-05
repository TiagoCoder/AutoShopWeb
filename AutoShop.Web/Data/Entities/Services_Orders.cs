using System.ComponentModel.DataAnnotations;

namespace AutoShop.Web.Data.Entities
{
    public class Services_Orders : IEntity
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public int OrderId { get; set; }

        public Service Service { get; set; }

        public Order Order { get; set; }

        public int Qtd { get; set; }

    }
}
