using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class ExchangeUnitsData : IExchangeUnitsData
    {

        private readonly ISqlDataAccess _db;

        public ExchangeUnitsData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<ExchangeUnitsModel> GetExchangeUnits()
        {
            string sql = "select * from dbo.ExchangeUnits";
            var data = await _db.LoadData<ExchangeUnitsModel, dynamic>(sql, new { });
            return data[0];
        }
    }
}
