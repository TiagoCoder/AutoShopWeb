using AutoShop.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Data.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetAllWithUser();

        Task<bool> DeleteEmployeeAsync(Employee employee);
    }
}
