using System.Windows;

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
