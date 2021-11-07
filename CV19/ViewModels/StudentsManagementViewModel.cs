using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class StudentsManagementViewModel : ViewModel
  {
    #region Title: string - Заголовок окна
    /// <summary>
    /// Заголовок окна
    /// </summary>
    private string _title = "Управление студентами";

    /// <summary>
    /// Заголовок окна
    /// </summary>
    public string Title
    {
      get => _title;
      set => Set(ref _title, value);
    } 
    #endregion
  }
}