using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Data
{
    public class Currency
    {

        string Name { get; set; }
        decimal ExchangeRate { get; set; }
        int Value { get; set; }
        int Unit { get; set; }
        int Amount { get; set; }
    }
}
