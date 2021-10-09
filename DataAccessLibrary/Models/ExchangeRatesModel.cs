using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ExchangeRatesModel
    {
        public int USD { get; set; }
        public int EUR { get; set; }
        public int CHF { get; set; }
        public int RUB { get; set; }
        public int CZK { get; set; }
        public int GBP { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
