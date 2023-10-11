using eAuction.Models;

namespace eAuction.Data.Services
{
    public interface IAccountsService
    {
        Task Register(UserModel user);
        Task Login(UserModel user);
        Task Logout();
    }
}
