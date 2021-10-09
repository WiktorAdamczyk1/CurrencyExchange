using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IExchangeUnitsData
    {
        Task<ExchangeUnitsModel> GetExchangeUnits();
    }
}