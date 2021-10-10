using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class UserWalletsData : IUserWalletsData
    {
        private readonly ISqlDataAccess _db;

        public UserWalletsData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<UserWalletsModel> GetUserWallet(string currentUserId)
        {
            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new ArgumentException($"'{nameof(currentUserId)}' cannot be null or empty.", nameof(currentUserId));
            }

            string sql = "select * from dbo.UserWallets where UserId = @UserId";
            return (await _db.LoadData<UserWalletsModel, dynamic>(sql, new { userId = currentUserId })).FirstOrDefault();
        }

        public Task UpdateUserWalletBalance(UserWalletsModel userWallet)
        {
            string sql = @"update dbo.UserWallets set PLN = @PLN, USD = @USD, EUR = @EUR, CHF = @CHF, RUB = @RUB, CZK = @CZK, GBP = @GBP where UserId = @UserId;";

            return _db.SaveData(sql, userWallet);
        }

        public Task InsertUserWallet(UserWalletsModel userWallet)
        {
            string sql = @"insert into dbo.UserWallets (UserId, PLN, USD, EUR, CHF, RUB, CZK, GBP)
                            values (@UserId, @PLN, @USD, @EUR, @CHF, @RUB, @CZK, @GBP);";

            return _db.SaveData(sql, userWallet);
        }
    }
}
