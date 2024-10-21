using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        Users Authenticate(string username, string password);
        string GenerateToken(Users user);
    }
}
