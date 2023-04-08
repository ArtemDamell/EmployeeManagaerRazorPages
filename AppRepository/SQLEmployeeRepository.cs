using System.Collections.Generic;
using System.Linq;
using FirstRazorApp.AppRepository;
using FirstRazorApp.Models;

namespace FirstRazorApp.Services
{
    public class SQLEmployeeRepository : IEmpoyeeRepository
    {
        private readonly AppDbContext _context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Searches for Employees based on a search term.
        /// </summary>
        /// <param name="searchTerm">The search term to use for the search.</param>
        /// <returns>A collection of Employees that match the search term.</returns>
        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return _context.Employees;
            }
            return _context.Employees.Where(x => x.Name.Contains(searchTerm) ||
                                          x.Email.Contains(searchTerm));
        }

        /// <summary>
        /// Retrieves all Employees from the database.
        /// </summary>
        public IEnumerable<Employee> GetAllEmployees() => _context.Employees;

        /// <summary>
        /// Retrieves an Employee object from the database based on the provided Id.
        /// </summary>
        public Employee GetEmployee(int id) => _context.Employees.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Updates an existing Employee in the database.
        /// </summary>
        /// <param name="updatedEmployee">The Employee to be updated.</param>
        /// <returns>The updated Employee.</returns>
        public Employee Update(Employee updatedEmployee)
        {
            var employee = _context.Employees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedEmployee;
        }

        /// <summary>
        /// Adds a new Employee to the database.
        /// </summary>
        /// <returns>
        /// The newly added Employee.
        /// </returns>
        public Employee Add(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee;
        }

        /// <summary>
        /// Deletes an employee from the database.
        /// </summary>
        /// <param name="id">The id of the employee to delete.</param>
        /// <returns>The deleted employee.</returns>
        public Employee Delete(int id)
        {
            Employee employee = _context.Employees.Find(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return employee;
        }

        /// <summary>
        /// Retrieves the number of employees in each department.
        /// </summary>
        /// <param name="dept">The department to filter by.</param>
        /// <returns>A list of Department and Count objects.</returns>
        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _context.Employees;

            if (dept.HasValue)
            {
                query = query.Where(e => e.Department == dept.Value);
            }

            return query.GroupBy(x => x.Department)
                .Select(p => new DeptHeadCount()
                {
                    Department = p.Key.Value,
                    Count = p.Count()
                }).ToList();
        }
    }
}
