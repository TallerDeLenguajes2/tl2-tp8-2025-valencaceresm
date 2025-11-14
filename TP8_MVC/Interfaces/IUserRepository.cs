using TP8_MVC.Models;

namespace TP8_MVC.Interfaces;

public interface IUserRepository
{
    Usuario? GetUser(string username, string password);
}
