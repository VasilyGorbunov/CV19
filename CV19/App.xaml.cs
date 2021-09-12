using System.Linq;
using System.Windows;
using CV19.Services;

namespace CV19
{
  public partial class App : Application
  {
    public static bool IsDedignMode { get; private set; } = true;

    protected override void OnStartup(StartupEventArgs e)
    {
      IsDedignMode = false;
      base.OnStartup(e);
    }
  }
}
