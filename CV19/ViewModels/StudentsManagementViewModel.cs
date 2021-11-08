using System.Collections.Generic;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models.Decanat;
using CV19.Services.Students;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class StudentsManagementViewModel : ViewModel
  {
    private readonly StudentsManager _studentsManager;

    #region Public Properties
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

    #region SelectedStudent : Student - Выбранный студент
    private Student _selectedStudent;

    /// <summary>
    /// Выбранный студент
    /// </summary>
    public Student SelectedStudent { get => _selectedStudent; set => Set(ref _selectedStudent, value); }
    #endregion

    public IEnumerable<Student> Students => _studentsManager.Students;
    public IEnumerable<Group> Groups => _studentsManager.Groups;
    #endregion

    #region Commands

    #region Редактирование студента
    private ICommand _editStudentCommand;

    /// <summary>
    /// Редактирование студента
    /// </summary>
    public ICommand EditStudentCommand => _editStudentCommand ??= new LambdaCommand(
      OnEditStudentCommandExecuted,
      CanEditStudentCommandExecute);

    private bool CanEditStudentCommandExecute(object p) => p is Student;

    private void OnEditStudentCommandExecuted(object p)
    {
      var student = (Student)p;

    }
    #endregion

    #region Создание нового студента
    private ICommand _createNewStudentCommand;

    /// <summary>
    /// Создание нового студента
    /// </summary>
    public ICommand CreateNewStudentCommand => _createNewStudentCommand ??= new LambdaCommand(
      OnCreateNewStudentCommandExecuted,
      CanCreateNewStudentCommandExecute);

    private static bool CanCreateNewStudentCommandExecute(object p) => p is Group;

    private void OnCreateNewStudentCommandExecuted(object p)
    {
      var group = (Group) p;

    }
    #endregion

    #endregion

    #region Constructors
    public StudentsManagementViewModel(StudentsManager studentsManager) => _studentsManager = studentsManager; 
    #endregion
  }
}