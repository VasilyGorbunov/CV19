using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CV19
{
  public static class Program
  {
    [STAThread]
    public static void Main(string[] args)
    {
      var app = new App();
      app.InitializeComponent();
      app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .UseContentRoot(App.CurrentDirectory)
        .ConfigureAppConfiguration((host, cfg) => cfg
          .SetBasePath(App.CurrentDirectory)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        )
        .ConfigureServices(App.ConfigureServices);
    }
  }
}