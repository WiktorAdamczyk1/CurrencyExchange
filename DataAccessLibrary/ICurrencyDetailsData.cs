using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface ICurrencyDetailsData
    {
        Task<int> CountCurrencyDetails();
        Task<List<CurrencyDetailsModel>> GetLast20CurrencyDetails();
        Task<List<CurrencyDetailsModel>> GetNewestCurrencyDetails();
        Task InsertCurrencyDetails(CurrencyDetailsModel currencyDetails);
        Task DeleteOldCurrencyDetails();
        Task<DateTime> GetNewestCurrencyDetailsTimestamp();
    }
}