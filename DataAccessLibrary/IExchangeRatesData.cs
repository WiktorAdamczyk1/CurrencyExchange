using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IExchangeRatesData
    {
        Task<List<ExchangeRatesModel>> GetExchangeRates();
        Task InsertExchangeRates(ExchangeRatesModel exchangeRates);
    }
}