// Services/EmployeeService.cs
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Employee> GetAll() => _repository.GetAll();

        public Employee GetById(int id) => _repository.GetById(id);

        public void Add(Employee employee) => _repository.Add(employee);

        public void Update(Employee employee) => _repository.Update(employee);

        public void Delete(int id) => _repository.Delete(id);
    }
}
