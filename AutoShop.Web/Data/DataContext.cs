using AutoShop.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoShop.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<VehicleInfo> VehicleInfo { get; set; }

        public DbSet<ClientVehicle> ClientVehicles { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Services_Orders> Services_Orders { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Services_Orders>(e =>
            {
                e.HasKey(v => new { v.ServiceId, v.OrderId });
            });
           

            // Habilitar a cascade delete rule
            var cascadeFKs = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            }

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Vehicles)
                .WithOne(w => w.Client)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
