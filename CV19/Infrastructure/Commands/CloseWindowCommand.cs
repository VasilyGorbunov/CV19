using System.Windows;
using CV19.Infrastructure.Commands.Base;

namespace CV19.Infrastructure.Commands
{
  public class CloseWindowCommand : Command
  {
    public override bool CanExecute(object parameter) => parameter is Window;

    public override void Execute(object parameter)
    {
      if(!CanExecute(parameter)) return;

      var window = (Window) parameter;
      window.Close();
    }
  }

  public class CloseDialogCommand : Command
  {
    public bool? DialogResult { get; set; }

    public override bool CanExecute(object parameter) => parameter is Window;

    public override void Execute(object parameter)
    {
      if (!CanExecute(parameter)) return;

      var window = (Window)parameter;
      window.DialogResult = DialogResult;
      window.Close();
    }
  }
}