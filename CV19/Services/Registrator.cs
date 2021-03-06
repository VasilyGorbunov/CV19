using CV19.Services.Interfaces;
using CV19.Services.Students;
using Microsoft.Extensions.DependencyInjection;

namespace CV19.Services
{
  public static class Registrator
  {
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
      services.AddSingleton<IDataService, DataService>();
      services.AddTransient<IAsyncDataService, AsyncDataService>();
      services.AddTransient<IWebServerService, HttpListenerWebServer>();

      services.AddSingleton<StudentsRepository>();
      services.AddSingleton<GroupsPepository>();
      services.AddSingleton<StudentsManager>();

      services.AddTransient<IUserDialogService, WindowsUserDialogService>();

      return services;
    }
  }
}