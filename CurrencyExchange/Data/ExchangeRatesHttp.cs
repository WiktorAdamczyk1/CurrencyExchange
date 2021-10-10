using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyExchange.Data
{
    public class ExchangeRatesHttp
    {

        public HttpClient Client { get; }

        public ExchangeRatesHttp(HttpClient client)
        {
            client.BaseAddress = new Uri("http://webtask.future-processing.com:8068/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;
        }


        public async Task<Currencies> OnGet()
        {
           return await Client.GetFromJsonAsync<Currencies>("currencies");
        }
    }
}
