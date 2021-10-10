using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class CurrencyDetailsData : ICurrencyDetailsData
    {
        private readonly ISqlDataAccess _db;

        public CurrencyDetailsData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<DateTime> GetNewestCurrencyDetailsTimestamp()
        {
            string sql = "select TOP 1 Timestamp from dbo.CurrencyDetails order by Timestamp DESC";

            return (await _db.LoadData<DateTime, dynamic>(sql, new { })).FirstOrDefault();
        }

        public Task<List<CurrencyDetailsModel>> GetNewestCurrencyDetails()
        {
            string sql = "select TOP 6 * from dbo.CurrencyDetails order by Timestamp DESC";

            return _db.LoadData<CurrencyDetailsModel, dynamic>(sql, new { });
        }

        public Task<List<CurrencyDetailsModel>> GetLast20CurrencyDetails()
        {
            string sql = "select TOP 120 * from dbo.CurrencyDetails order by Timestamp DESC";

            return _db.LoadData<CurrencyDetailsModel, dynamic>(sql, new { });
        }

        public Task InsertCurrencyDetails(CurrencyDetailsModel currencyDetails)
        {
            string sql = @"insert into dbo.CurrencyDetails (Name, Code, Unit, PurchasePrice, SellPrice, AveragePrice, Timestamp)
                            values (@Name, @Code, @Unit, @PurchasePrice, @SellPrice, @AveragePrice, @Timestamp);";

            return _db.SaveData(sql, currencyDetails);
        }

        public Task DeleteOldCurrencyDetails() // Test this
        {
            string sql = @"WITH del AS (SELECT * FROM dbo.CurrencyDetails ORDER BY Timestamp DESC OFFSET 120 ROWS) DELETE FROM del;";

            return _db.SaveData(sql, new { });
        }

        public async Task<int> CountCurrencyDetails()
        {
            string sql = @"SELECT COUNT(*) FROM dbo.AspNetUsers;";

            return (await _db.LoadData<int, dynamic>(sql, new { })).FirstOrDefault();
        }

    }
}
