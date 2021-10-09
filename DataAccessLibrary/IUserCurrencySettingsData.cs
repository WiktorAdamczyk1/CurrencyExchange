using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IUserCurrencySettingsData
    {
        Task<UserCurrencySettingsModel> GetUserCurrencySettings(string currentUserId);
        Task InsertUserCurrencySettings(UserCurrencySettingsModel userCurrencySettings);
        Task UpdateUserCurrencySettings(UserCurrencySettingsModel userCurrencySettings);
    }
}