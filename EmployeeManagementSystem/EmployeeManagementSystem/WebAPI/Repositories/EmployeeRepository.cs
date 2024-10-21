// Repositories/EmployeeRepository.cs
using EmployeeManagementSystem.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = new List<Employee>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Employees", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            Position = reader.GetString("Position"),
                            Salary = reader.GetDecimal("Salary")
                        });
                    }
                }
            }

            return employees;
        }

        public Employee GetById(int id)
        {
            Employee employee = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Employees WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new Employee
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                Position = reader.GetString("Position"),
                                Salary = reader.GetDecimal("Salary")
                            };
                        }
                    }
                }
            }

            return employee;
        }

        public void Add(Employee employee)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Employees (Name, Position, Salary) VALUES (@Name, @Position, @Salary)", connection))
                {
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Employee employee)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE Employees SET Name = @Name, Position = @Position, Salary = @Salary WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM Employees WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
