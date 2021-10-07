using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class WebServerViewModel: ViewModel
  {
    private readonly IWebServerService _server;

    public WebServerViewModel(IWebServerService server)
    {
      _server = server;
    }

    #region Properties

    #region Enabled
    
    public bool Enabled
    {
      get => _server.Enabled;
      set
      {
        _server.Enabled = value;
        OnPropertyChanged(nameof(Enabled));
      }
    }
    #endregion

    #endregion

    #region Commands

    #region Start Server
    private ICommand _startCommand;

    public ICommand StartCommand => _startCommand
      ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

    private bool CanStartCommandExecute(object p) => !Enabled;

    private void OnStartCommandExecuted(object p)
    {
      _server.Start();
      OnPropertyChanged(nameof(Enabled));
    }
    #endregion

    #region Stop Server
    private ICommand _stopCommand;

    public ICommand StopCommand => _stopCommand
      ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

    private bool CanStopCommandExecute(object p) => Enabled;

    private void OnStopCommandExecuted(object p)
    {
      _server.Stop();
      OnPropertyChanged(nameof(Enabled));
    }
    #endregion

    #endregion
  }
}