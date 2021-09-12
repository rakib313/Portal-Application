using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalApplication.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;
        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();

            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int ID)
        {
            return context.Employees.Find(ID);
        }

        public async Task<Employee> Update(Employee employeeChanges)
        {
            // var employee = context.Employees.Attach(employeeChanges);
            // employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var employee = context.Update(employeeChanges);
            await context.SaveChangesAsync();
            return employeeChanges;
        }
    }
}
