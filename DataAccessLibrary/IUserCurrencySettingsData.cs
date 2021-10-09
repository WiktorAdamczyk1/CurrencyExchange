using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IUserCurrencySettingsData
    {
        Task<UserCurrencySettingsModel> GetUserWallet(string currentUserId);
        Task InsertUserCurrencySettings(UserCurrencySettingsModel userCurrencySettings);
        Task UpdateUserCurrencySettings(UserCurrencySettingsModel userCurrencySettings);
    }
}