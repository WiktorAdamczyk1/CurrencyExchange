using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ExchangeRatesModel
    {
        public decimal USD { get; set; }
        public decimal EUR { get; set; }
        public decimal CHF { get; set; }
        public decimal RUB { get; set; }
        public decimal CZK { get; set; }
        public decimal GBP { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
