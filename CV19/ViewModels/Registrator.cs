using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModels
{
  public static class Registrator
  {
    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
      services.AddSingleton<MainWindowViewModel>();
      services.AddSingleton<CountriesStatisticViewModel>();
      services.AddSingleton<WebServerViewModel>();

      services.AddTransient<StudentsManagementViewModel>();
      return services;
    }
  }
}