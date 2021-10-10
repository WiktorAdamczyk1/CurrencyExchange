using System.Threading.Tasks;

namespace CurrencyExchange.Hubs
{
    public interface INotificationHub
    {
        Task SendMessage(string alert);
    }
}