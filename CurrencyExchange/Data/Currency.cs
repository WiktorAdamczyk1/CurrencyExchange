using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Data
{
    public class Currency
    {

        //public string Name { get; set; }
        //public decimal ExchangeRate { get; set; }
        //public int Value { get; set; }
        //public int Unit { get; set; }
        //public int Amount { get; set; }

        public string name { get; set; }
        public string code { get; set; }
        public int unit { get; set; }
        public decimal purchasePrice { get; set; }
        public decimal sellPrice { get; set; }
        public decimal averagePrice { get; set; }

    }

    public class Currencies
    {
        public DateTime publicationDate { get; set; }
        public Currency[] items { get; set; }
    }
}
