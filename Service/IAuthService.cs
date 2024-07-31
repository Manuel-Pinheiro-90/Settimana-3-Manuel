using Settimana_3_Manuel.Models;

namespace Settimana_3_Manuel.Service
{
    public interface IAuthService
    {
        User Login(string username, string password);
        User CreateUser(User user);
        User Register(User user);



    }
}
