using System.Globalization;
using System.Linq;

const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

static async Task<Stream> GetDataStream()
{
    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
    return await response.Content.ReadAsStreamAsync();
}

static IEnumerable<string> GetDataLines()
{
    using Stream? data_stream = GetDataStream().Result;
    using StreamReader? data_reader = new StreamReader(data_stream);

    while(!data_reader.EndOfStream)
    {
        string? line = data_reader.ReadLine();
        if (string.IsNullOrWhiteSpace(line)) continue;
        yield return line
            .Replace("Korea,", "Korea -")
            .Replace("Bonaire,", "Bonaire -")
            .Replace("Saint Helena,", "Saint Helena -");
    }
}

static DateTime[] GetDates() => GetDataLines()
    .First()
    .Split(',')
    .Skip(4)
    .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
    .ToArray();


static IEnumerable<(string Country, string Province, int[] Counts )> GetData()
{
    var lines = GetDataLines()
        .Skip(1)
        .Select(line => line.Split(','));

    foreach( var row in lines )
    {
        var province = row[0].Trim();
        var country_name = row[1].Trim(' ', '"');
        var counts = row.Skip(4).Select(int.Parse).ToArray();
        yield return (country_name, province, counts);
    }
}


//foreach (string line in GetDataLines())
//{
//    Console.WriteLine(line);
//}

// Console.WriteLine(string.Join("\r\n", GetDates()));

var russia_data = GetData().First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count) => $"{date:dd:MM:yyyy} - {count}")));

Console.ReadLine();
