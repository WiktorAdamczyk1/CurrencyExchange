using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class ExchangeRatesData : IExchangeRatesData
    {
        private readonly ISqlDataAccess _db;

        public ExchangeRatesData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<ExchangeRatesModel>> GetExchangeRates()
        {
            string sql = "select * from dbo.ExchangeRates";

            return _db.LoadData<ExchangeRatesModel, dynamic>(sql, new { });
        }

        public Task InsertExchangeRates(ExchangeRatesModel exchangeRates)
        {
            string sql = @"insert into dbo.ExchangeRates (USD, EUR, CHF, RUB, CZK, GBP, Timestamp)
                            values (@USD, @EUR, @CHF, @RUB, @CZK, @GBP, @Timestamp);";

            return _db.SaveData(sql, exchangeRates);
        }
    }
}
