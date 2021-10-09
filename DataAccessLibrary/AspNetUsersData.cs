using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class AspNetUsersData : IAspNetUsersData
    {

        private readonly ISqlDataAccess _db;

        public AspNetUsersData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<string> GetUserId(string userName)
        {
            string sql = "select Id from dbo.AspNetUsers where UserName = @UserName";
            var data = await _db.LoadData<AspNetUsersModel, dynamic>(sql, new { UserName = userName });
            return data[0].Id;
        }
    }
}
