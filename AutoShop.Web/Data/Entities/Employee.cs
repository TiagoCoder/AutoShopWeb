namespace AutoShop.Web.Data.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public string NIB { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }
    }
}
