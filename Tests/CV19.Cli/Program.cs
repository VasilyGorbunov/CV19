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
        yield return line;
    }
}


foreach (string line in GetDataLines())
{
    Console.WriteLine(line);
}

Console.ReadLine();
