using AutoShop.Web.Data.Entities;
using AutoShop.Web.Data.Repositories;
using AutoShop.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IClientRepository _clientRepository;

        public SeedDB(DataContext context, IUserHelper userHelper, IClientRepository clientRepository)
        {
            _context = context;
            _userHelper = userHelper;
            _clientRepository = clientRepository;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Employee");
            await _userHelper.CheckRoleAsync("Client");

            var user = await _userHelper.GetUserByEmailAsync("tiagotorres14516@gmail.com");
            if (user == null)
            {
                #region Seed the Admin

                user = new User
                {
                    FirstName = "Tiago",
                    LastName = "Torres",
                    Email = "tiagotorres14516@gmail.com",
                    UserName = "tiagotorres14516@gmail.com",
                    Gender = 'M',
                    PhoneNumber = "222222222",
                    BirthDate = new DateTime(1996, 11, 02),
                    Address = "Admin Street"
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in Seeder");
                }

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

                var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, "Admin");
                }
                #endregion
                #region Seed the Employee
                user = new User
                {
                    FirstName = "John",
                    LastName = "Worker",
                    Email = "employee@yopmail.com",
                    UserName = "employee@yopmail.com",
                    Gender = 'M',
                    PhoneNumber = "333333333",
                    BirthDate = new DateTime(1995, 4, 25),
                    Address = "Employees Street"
                };

                result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in Seeder");
                }

                token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

                isInRole = await _userHelper.IsUserInRoleAsync(user, "Employee");
                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, "Employee");
                }
                #endregion
                #region Seed Client
                user = new User
                {
                    FirstName = "James",
                    LastName = "Client",
                    Email = "client@yopmail.com",
                    UserName = "client@yopmail.com",
                    Gender = 'M',
                    PhoneNumber = "444444444",
                    BirthDate = new DateTime(1994, 5, 20),
                    Address = "Clients Street"
                };

                result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in Seeder");
                }

                token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

                isInRole = await _userHelper.IsUserInRoleAsync(user, "Client");
                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, "Client");
                }
                #endregion
            }
            if (_context.Employees.Count() == 0)
            {
                user = await _userHelper.GetUserByEmailAsync("employee@yopmail.com");
                if (user != null)
                {
                    _context.Employees.Add(new Employee { UserId = user.Id, NIB = "222222222" });
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.Clients.Count() == 0)
            {
                user = await _userHelper.GetUserByEmailAsync("client@yopmail.com");
                if(user != null)
                {
                    _context.Clients.Add(new Client { UserId = user.Id, NIB = "111111111" });
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.VehicleInfo.Count() == 0)
            {
                #region Seed Opel
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Opel", Model = "Astra", Year = "1990" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Opel", Model = "Corsa", Year = "1991" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Opel", Model = "Insignia", Year = "1992" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Opel", Model = "Zafira", Year = "1993" });
                #endregion
                #region Mitsubishi
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mitsubishi", Model = "Challenger", Year = "2000" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mitsubishi", Model = "Colt", Year = "2001" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mitsubishi", Model = "Lancer", Year = "2002" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mitsubishi", Model = "Mirage", Year = "2003" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mitsubishi", Model = "Sigma", Year = "2004" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mitsubishi", Model = "Pajero", Year = "2000" });
                #endregion
                #region Seed Chevrolet
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Chevrolet", Model = "Camaro", Year = "1999" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Chevrolet", Model = "Corvet", Year = "1989" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Chevrolet", Model = "Silverado", Year = "1978" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Chevrolet", Model = "Impala", Year = "1990" });
                #endregion
                #region Citroen
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Citroen", Model = "C2", Year = "2004" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Citroen", Model = "C3", Year = "2005" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Citroen", Model = "C5", Year = "2008" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Citroen", Model = "Berlingo", Year = "2002" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Citroen", Model = "Picasso", Year = "2011" });
                #endregion
                #region Renault
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Renault", Model = "Clio", Year = "2003" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Renault", Model = "Megane", Year = "2004" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Renault", Model = "Kangoo", Year = "2005" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Renault", Model = "Master", Year = "2010" });
                #endregion
                #region Volvo
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Volvo", Model = "S60", Year = "2011" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Volvo", Model = "V60", Year = "2012" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Volvo", Model = "V90", Year = "2012" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Volvo", Model = "XC40", Year = "2005" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Volvo", Model = "XC60", Year = "2008" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Volvo", Model = "XC90", Year = "2018" });
                #endregion
                #region BMW
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "230i", Year = "2010" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "M2", Year = "2006" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "330i", Year = "2009" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "M340i", Year = "2010" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "430i", Year = "2012" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "440i", Year = "2015" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "X1", Year = "2001" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "X2", Year = "2002" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "X3", Year = "2003" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "X4", Year = "2004" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "BMW", Model = "X5", Year = "2005" });
                #endregion
                #region Mercedes
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mercedez-Benz", Model = "A220", Year = "2010" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mercedez-Benz", Model = "AMG C43", Year = "2012" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mercedez-Benz", Model = "AMG C63", Year = "2014" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mercedez-Benz", Model = "AMG G63", Year = "2016" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mercedez-Benz", Model = "C300", Year = "2006" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mercedez-Benz", Model = "E350", Year = "2007" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mercedez-Benz", Model = "AMG E53", Year = "2008" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mercedez-Benz", Model = "AMG E63", Year = "2014" });
                _context.VehicleInfo.Add(new VehicleInfo { Brand = "Mercedez-Benz", Model = "Sprinter", Year = "2000" });
                #endregion
                await _context.SaveChangesAsync();
            }
            if (_context.ClientVehicles.Count() == 0)
            {
                var client = _clientRepository.GetAll().Where(c => c.UserId == user.Id).FirstOrDefault();
                if(client != null)
                {
                    _context.ClientVehicles.Add(new ClientVehicle { ClientId = client.Id, VehicleInfoId = 1, Registration = "AA-BB-00", Color ="Black"});
                    await _context.SaveChangesAsync();
                }
            }
            if(_context.Services.Count() == 0)
            {
                _context.Services.Add(new Service { Description = "Oil Change", Price= 50});
                _context.Services.Add(new Service { Description = "Tyre Replacement", Price = 120 });
                _context.Services.Add(new Service { Description = "Battery Replacement", Price = 100 });
                await _context.SaveChangesAsync();
            }
            if (_context.Orders.Count() == 0)
            {
                _context.Orders.Add(new Order { ClientId = 1, ClientVehicleId = 1, OrderDate = DateTime.Now, DeliveryDate = DateTime.Now.AddDays(4) });
                await _context.SaveChangesAsync();
            }
            if (_context.Services_Orders.Count() == 0)
            {
                _context.Services_Orders.Add(new Services_Orders { OrderId = 1, ServiceId = 1, Qtd = 1 });
                _context.Services_Orders.Add(new Services_Orders { OrderId = 1, ServiceId = 2 , Qtd = 4});
                _context.Services_Orders.Add(new Services_Orders { OrderId = 1, ServiceId = 3, Qtd = 1 });
                await _context.SaveChangesAsync();
            }
        }
    }
}
