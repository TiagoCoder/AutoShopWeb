namespace AutoShop.Web.Data.Entities
{
    public class VehicleInfo : IEntity
    {
        public int Id { get; set; }

        public string Year { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
    }
}
