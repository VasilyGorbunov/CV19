using CV19.Models;
using System.IO;
using System.Net.Http;

namespace CV19.Services
{
    internal class DataService
    {
        private const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_DataSourceAddress, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        public IEnumerable<CountryInfo> GetData()
        {

        }
    }
}
