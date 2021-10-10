using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class UserWalletsModel
    {
        public string UserId { get; set; }
        public decimal PLN { get; set; }
        public int USD { get; set; }
        public int EUR { get; set; }
        public int CHF { get; set; }
        public int RUB { get; set; }
        public int CZK { get; set; }
        public int GBP { get; set; }
    }
}
