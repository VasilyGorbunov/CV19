using System.Collections.Generic;
using CV19.Models.Decanat;
using CV19.Services.Students;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class StudentsManagementViewModel : ViewModel
  {
    private readonly StudentsManager _studentsManager;

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

    #region SelectedGroup: Group - Выбранная группа
    /// <summary>
    /// Выбранная группа
    /// </summary>
    private Group _selectedGroup;

    /// <summary>
    /// Выбранная группа
    /// </summary>
    public Group SelectedGroup
    {
      get => _selectedGroup;
      set => Set(ref _selectedGroup, value);
    } 
    #endregion

    public IEnumerable<Student> Students => _studentsManager.Students;
    public IEnumerable<Group> Groups => _studentsManager.Groups;

    public StudentsManagementViewModel(StudentsManager studentsManager) => _studentsManager = studentsManager;
  }
}