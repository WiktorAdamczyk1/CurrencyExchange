using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class UserCurrencySettingsData : IUserCurrencySettingsData
    {

        private readonly ISqlDataAccess _db;

        public UserCurrencySettingsData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<UserCurrencySettingsModel> GetUserCurrencySettings(string currentUserId)
        {
            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new ArgumentException($"'{nameof(currentUserId)}' cannot be null or empty.", nameof(currentUserId));
            }

            string sql = "select * from dbo.UserCurrencySettings where UserId = @UserId";
            var data = await _db.LoadData<UserCurrencySettingsModel, dynamic>(sql, new { UserId = currentUserId });
            return data[0];
        }

        public Task UpdateUserCurrencySettings(UserCurrencySettingsModel userCurrencySettings)
        {
            string sql = @"update dbo.UserCurrencySettings set USD = @USD, EUR = @EUR, CHF = @CHF, RUB = @RUB, CZK = @CZK, GBP = @GBP;";

            return _db.SaveData(sql, userCurrencySettings);
        }

        public Task InsertUserCurrencySettings(UserCurrencySettingsModel userCurrencySettings)
        {
            string sql = @"insert into dbo.UserCurrencySettings (UserId, USD, EUR, CHF, RUB, CZK, GBP)
                            values (@UserId, @USD, @EUR, @CHF, @RUB, @CZK, @GBP);";

            return _db.SaveData(sql, userCurrencySettings);
        }

    }
}
