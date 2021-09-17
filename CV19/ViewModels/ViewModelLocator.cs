using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModels
{
  public class ViewModelLocator
  {
    public MainWindowViewModel MainWindowModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
  }
}