using BasketStatsApi.Models;

namespace BasketStatsApi.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string passwprd);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);

    }
}
