using TestSystemAPI.DTOs;
using TestSystemAPI.Models;

namespace TestSystemAPI.Interfaces
{
    public interface IUserService
    {
        User GetUserByLoginAndPassword(string login, string password);
    }
}
