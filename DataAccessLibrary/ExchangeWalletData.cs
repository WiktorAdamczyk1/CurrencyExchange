using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class ExchangeWalletData : IExchangeWalletData
    {
        private readonly ISqlDataAccess _db;

        public ExchangeWalletData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<ExchangeWalletModel> GetExchangeWallet()
        {
            string sql = "select * from dbo.ExchangeWallet";
            var data = await _db.LoadData<ExchangeWalletModel, dynamic>(sql, new { });
            return data[0];
        }

        public Task UpdateExchangeWalletBalance(ExchangeWalletModel exchangeWallet)
        {
            string sql = @"update dbo.ExchangeWallet set PLN = @PLN, USD = @USD, EUR = @EUR, CHF = @CHF, RUB = @RUB, CZK = @CZK, GBP = @GBP;";

            return _db.SaveData(sql, exchangeWallet);
        }
    }
}
