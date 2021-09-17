using System;
using System.Windows;
using CV19.Services;
using CV19.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CV19
{
  public partial class App
  {
    private static IHost _host;

    public static bool IsDedignMode { get; private set; } = true;

    protected override async void OnStartup(StartupEventArgs e)
    {
      IsDedignMode = false;
      var host = Host;
      base.OnStartup(e);

      await host.StartAsync().ConfigureAwait(false);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
      base.OnExit(e);
      var host = Host;
      await host.StopAsync().ConfigureAwait(false);
      host.Dispose();
      _host = null;
    }

    public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
      services.AddSingleton<DataService>();

      services.AddSingleton<CountriesStatisticViewModel>();

    }
  }
}
