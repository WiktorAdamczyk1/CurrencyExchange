using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IExchangeWalletData
    {
        Task<ExchangeWalletModel> GetExchangeWallet();
        Task UpdateExchangeWalletBalance(ExchangeWalletModel exchangeWallet);
    }
}