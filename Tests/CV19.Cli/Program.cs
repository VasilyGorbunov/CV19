const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

HttpClient client = new HttpClient();

HttpResponseMessage response = client.GetAsync(data_url).GetAwaiter().GetResult();
string csv_str = response.Content.ReadAsStringAsync().Result;

Console.ReadLine();
