using System.Collections.Generic;

namespace AutoShop.Web.Data.Entities
{
    public class Client : IEntity
    {
        public int Id { get; set; }

        public string NIB { get; set; }

        public IEnumerable<ClientVehicle> Vehicles { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }
    }
}
