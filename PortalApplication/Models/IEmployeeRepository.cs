using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalApplication.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int ID);
        // Retrive all Employees
        IEnumerable<Employee> GetAllEmployees();
        Employee Add(Employee employee);
        Task<Employee> Update(Employee employeeChanges);
        Employee Delete(int id);
    }
}
