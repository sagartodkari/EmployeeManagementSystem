using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IUserRepository
    {
        Users GetUserByUsername(string username);
    }
}
