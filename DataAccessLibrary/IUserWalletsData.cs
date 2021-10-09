using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IUserWalletsData
    {
        Task<UserWalletsModel> GetUserWallet(string currentUserId);
        Task UpdateUserWalletBalance(UserWalletsModel userWallet);
    }
}