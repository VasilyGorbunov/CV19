using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CV19.Models;
using CV19.Services.Interfaces;

namespace CV19.Services
{
  public class DataService: IDataService
  {
    public DataService()
    {
      
    }

    private const string _dataSourceAddress =
      @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

    private static async Task<Stream> GetDataStream()
    {
      var client = new HttpClient();
      var response = await client.GetAsync(_dataSourceAddress, HttpCompletionOption.ResponseHeadersRead);
      return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    private static IEnumerable<string> GetDataLines()
    {
      using var dataStream =
        (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream)).Result;
      using var dataReader = new StreamReader(dataStream);

      while (!dataReader.EndOfStream)
      {
        var line = dataReader.ReadLine();
        if (string.IsNullOrWhiteSpace(line)) continue;
        yield return line
          .Replace("Korea,", "Korea -")
          .Replace("Bonaire,", "Bonaire -")
          .Replace("Saint Helena,", "Saint Helena -");
      }
    }

    private static DateTime[] GetDates() => GetDataLines()
      .First()
      .Split(',')
      .Skip(4)
      .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
      .ToArray();

    private static IEnumerable<(
      string province, 
      string country, 
      (double lat, double lon) place, 
      int[] counts)> GetCountriesData()
    {
      var lines = GetDataLines()
        .Skip(1)
        .Select(line => line.Split(','));

      foreach (var row in lines)
      {
        double latitude;
        double longitude;

        var province = row[0].Trim();
        var countryName = row[1].Trim(' ', '"');
        latitude = string.IsNullOrWhiteSpace(row[2]) ? 0.0 : double.Parse(row[2], CultureInfo.InvariantCulture);
        longitude = string.IsNullOrWhiteSpace(row[3]) ? 0.0 : double.Parse(row[3], CultureInfo.InvariantCulture);
        var counts = row.Skip(4).Select(int.Parse).ToArray();

        yield return (province, countryName, (latitude, longitude), counts);
      }

    }

    public IEnumerable<CountryInfo> GetData()
    {
      var dates = GetDates();

      var data = GetCountriesData().GroupBy(d => d.country);

      foreach (var countryInfo in data)
      {
        var country = new CountryInfo
        {
          Name = countryInfo.Key,
          Provinces = countryInfo.Select(c => new PlaceInfo
          {
            Name = c.province,
            Location = new Point(c.place.lat, c.place.lon),
            Counts = dates.Zip(c.counts, (date, count) => new ConfirmedCount
            {
              Date = date,
              Count = count
            })
          })
        };
        yield return country;
      }
    }
  }
}