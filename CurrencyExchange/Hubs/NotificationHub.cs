using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Hubs
{
    public class NotificationHub : Hub<INotificationHub>
    {
        public async Task SendMessage(string alert)
        {
            await Clients.All.SendMessage(alert);
        }
    }
}
