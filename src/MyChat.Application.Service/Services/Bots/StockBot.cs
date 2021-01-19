using MyChat.Application.Service.Contracts.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net;

namespace MyChat.Application.Service.Services.Bots
{
    public class StockBot : IChatBot
    {
        public async Task<string> CallAsync(string param1)
        {
            if (string.IsNullOrEmpty(param1))
                throw new ArgumentException("Parameter cannot be null or empty");

            var baseUrl = new Uri($"https://stooq.com/q/l/?s={param1}&f=sd2t2ohlcv&h&e=csv");

            var restClient = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);

            var response = await restClient.ExecuteAsync<string>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException("An error occurred during the request");
            }

            var data = response.Content;

            IList<string> lines = data.Split("\r\n").ToList();
            if (lines.Count <= 1)
                return "Sorry 🤔, no information was obtained!";

            string infoStock = lines[1];

            IList<string> infoStockData = infoStock.Split(",").ToList();

            if (infoStockData.Count < 4 || infoStockData[3] == "N/D")
                return "Sorry, this code was not found!";


            return $"{infoStockData[0]} quote is ${infoStockData[3]} per share.";
        }
    }
}
