using AutoShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Employee> GetAllWithUser()
        {
            return _context.Employees.Include(e => e.User);
        }

        public async Task<bool> DeleteEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.Users.Remove(employee.User);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
