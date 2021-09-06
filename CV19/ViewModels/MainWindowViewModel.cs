using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class MainWindowViewModel: ViewModel
  {
    #region Свойства

    #region Заголовок окна
    private string _title = "Анализ статистики CV19";

    /// <summary>
    /// Заголовок окна
    /// </summary>
    public string Title
    {
      get => _title;
      set => Set(ref _title, value);
    }
    #endregion

    #region Статус программы

    private string _status = "Готов!";
    /// <summary>
    /// Статус программы
    /// </summary>
    public string Status
    {
      get => _status;
      set => Set(ref _status, value);
    }

    #endregion

    #endregion

    #region Команды

    #region CloseApplicationCommand - Закрыть программу
    /// <summary>
    /// Закрыть программу
    /// </summary>
    public ICommand CloseApplicationCommand { get; }

    private void OnCloseApplicationCommandExecuted(object p)
    {
      Application.Current.Shutdown();
    }

    private bool CanCloseApplicationCommandExecute(object p) => true; 
    #endregion

    #endregion

    #region Конструктор

    public MainWindowViewModel()
    {
      #region Команды

      CloseApplicationCommand = new LambdaCommand(
        OnCloseApplicationCommandExecuted, 
        CanCloseApplicationCommandExecute);

      #endregion
    }

    #endregion
  }
}