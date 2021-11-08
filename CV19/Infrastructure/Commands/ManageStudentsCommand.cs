using System.Windows;
using CV19.Infrastructure.Commands.Base;
using CV19.Views.Windows;

namespace CV19.Infrastructure.Commands
{
  public class ManageStudentsCommand : Command
  {
    private StudentsManagementWindow _window;

    public override bool CanExecute(object? parameter) => _window == null;

    public override void Execute(object? parameter)
    {
      var window = new StudentsManagementWindow
      {
        Owner = Application.Current.MainWindow
      };

      window.Closed += OnWindowClosed;
      _window = window;

      _window.ShowDialog();
    }

    private void OnWindowClosed(object sender, System.EventArgs e)
    {
      ((Window) sender).Closed -= OnWindowClosed;
      _window = null;
    }
  }
}