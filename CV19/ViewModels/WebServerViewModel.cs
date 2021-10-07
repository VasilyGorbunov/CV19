using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class WebServerViewModel: ViewModel
  {
    #region Properties

    #region Enabled
    private bool _enabled;

    public bool Enabled
    {
      get => _enabled;
      set => Set(ref _enabled, value);
    }
    #endregion

    #endregion

    #region Commands

    #region Start Server
    private ICommand _startCommand;

    public ICommand StartCommand => _startCommand
      ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

    private bool CanStartCommandExecute(object p) => !_enabled;

    private void OnStartCommandExecuted(object p)
    {
      Enabled = true;
    }
    #endregion

    #region Stop Server
    private ICommand _stopCommand;

    public ICommand StopCommand => _stopCommand
      ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

    private bool CanStopCommandExecute(object p) => _enabled;

    private void OnStopCommandExecuted(object p)
    {
      Enabled = false;
    }
    #endregion

    #endregion
  }
}