namespace AutoShop.Web.Data.Entities
{
    public class ClientVehicle : IEntity
    {
        public int Id { get; set; }

        public string Registration { get; set; }

        public VehicleInfo VehicleInfo { get; set; }

        public int VehicleInfoId { get; set; }

        public string Color { get; set; }

        public Client Client { get; set; }

        public int ClientId { get; set; }
    }
}
