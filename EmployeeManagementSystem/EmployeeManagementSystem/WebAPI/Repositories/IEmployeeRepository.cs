// Repositories/IEmployeeRepository.cs
using EmployeeManagementSystem.Models;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
    }
}
