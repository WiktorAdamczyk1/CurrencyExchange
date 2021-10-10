using CurrencyExchange.Hubs;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyExchange.Data
{
    public class ExchangeRatesHostedService : BackgroundService, IHostedService
    {
        private int executionCount = 0;
        private int delayAfterExecute = 20000;
        private bool connectionSuccessful = false;
        private bool receivedOldDetails = false;
        private int receivedOldDetailsTryCount = 20;

        private readonly ExchangeRatesHttp _exchangeRatesHttp;
        private readonly ICurrencyDetailsData _currencyDetailsData;
        private readonly IHubContext<NotificationHub, INotificationHub> _notificationHub;

        public ExchangeRatesHostedService(ExchangeRatesHttp exchangeRatesHttp,
                                            IHubContext<NotificationHub, INotificationHub> notificationHub,
                                            ICurrencyDetailsData currencyDetailsData)
        {
            _exchangeRatesHttp = exchangeRatesHttp;
            _currencyDetailsData = currencyDetailsData;
            _notificationHub = notificationHub;
        }

        private async Task<string> FetchNewExchangeRates()
        {
            Currencies exchangeRates = null;

            executionCount++;
            if (executionCount > 50)
            {
                await _currencyDetailsData.DeleteOldCurrencyDetails();
                executionCount = 0;
            }

            try
            {
                exchangeRates = await _exchangeRatesHttp.OnGet();
                if (!connectionSuccessful)
                {
                    await _notificationHub.Clients.All.SendMessage("connectionResumed");
                    connectionSuccessful = true;
                }
            }
            catch(WebException ex)
            {
                connectionSuccessful = false;
                return "connectionError";
            }

            
            if (connectionSuccessful)
            {
                // Did not receive new info after 25 seconds, will now try 20 times every 2 seconds to get updated info
                // After 20 failes trading will be halted untill new details are received
                var oldPublicationDate = await _currencyDetailsData.GetNewestCurrencyDetailsTimestamp();
                if (oldPublicationDate >= exchangeRates.publicationDate)
                {
                    return "receivedOldDetails";
                }

                foreach (var currency in exchangeRates.items)
                {
                    await _currencyDetailsData.InsertCurrencyDetails(new CurrencyDetailsModel
                    {
                        Name = currency.name,
                        Code = currency.code,
                        Unit = currency.unit,
                        PurchasePrice = currency.purchasePrice,
                        AveragePrice = currency.averagePrice,
                        SellPrice = currency.sellPrice,
                        Timestamp = exchangeRates.publicationDate
                    });
                }
                return "updateDetails";
            }

            return string.Empty;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await FetchNewExchangeRates();
                Console.WriteLine(result);
                if(result == "receivedOldDetails")
                {

                    if (!receivedOldDetails)
                    {
                        receivedOldDetails = true;
                        delayAfterExecute = 2000;
                    }
                    if (receivedOldDetailsTryCount <= 0)
                    {
                        delayAfterExecute = 20000;
                        //stop trading
                        connectionSuccessful = false;
                        await _notificationHub.Clients.All.SendMessage("connectionError");
                    }
                    else receivedOldDetailsTryCount--;
                }
                else
                {
                    if (receivedOldDetails == true)
                    {
                        receivedOldDetails = false;
                        delayAfterExecute = 20000;
                        receivedOldDetailsTryCount = 20;
                    }
                    await _notificationHub.Clients.All.SendMessage(result);
                }
                await Task.Delay(delayAfterExecute);
            }
        }
    }
}
