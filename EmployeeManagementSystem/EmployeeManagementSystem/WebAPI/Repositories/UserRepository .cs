using MySql.Data.MySqlClient;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Users GetUserByUsername(string username)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Users WHERE Username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Users
                            {
                                Id = (int)reader["Id"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"] // Ideally, this should be hashed
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
