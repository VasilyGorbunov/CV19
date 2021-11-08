using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using CV19.Infrastructure.Commands;
using CV19.Models.Decanat;
using CV19.Services.Interfaces;
using CV19.Services.Students;
using CV19.ViewModels.Base;
using CV19.Views.Windows;

namespace CV19.ViewModels
{
  public class StudentsManagementViewModel : ViewModel
  {
    private readonly StudentsManager _studentsManager;
    private readonly IUserDialogService _userDialog;

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
      if (_userDialog.Edit(p))
      {
        _studentsManager.Update((Student)p);
        _userDialog.ShowInformation("Студент отредактирован", "Менеджер студентов");
      }
      else
      {
        _userDialog.ShowWarning("Отказ от редактирования", "Менеджер студентов");
      }
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
      var group = (Group)p;

      var student = new Student();

      if (!_userDialog.Edit(student) || _studentsManager.Create(student, group.Name))
      {
        OnPropertyChanged(nameof(Students));
        return;
      }

      if (_userDialog.Confirm("Не удалось создать студента. Повторить", "Менеджер студентов"))
        OnCreateNewStudentCommandExecuted(p);


    }
    #endregion

    #region Тестовая команда
    private ICommand _testCommand;

    /// <summary>
    /// Тестовая команда
    /// </summary>
    public ICommand TestCommand => _testCommand ??= new LambdaCommand(
      OnTestCommandExecuted,
      CanTestCommandExecute);

    private static bool CanTestCommandExecute(object p) => true;

    private void OnTestCommandExecuted(object p)
    {
      var value = _userDialog.GetStringValue("Введите строку", "123", "Значение по умолчанию");
      _userDialog.ShowInformation($"Введено {value}", "123");
    }
    #endregion

    #endregion

    #region Constructors
    public StudentsManagementViewModel(StudentsManager studentsManager, IUserDialogService userDialog)
    {
      _studentsManager = studentsManager;
      _userDialog = userDialog;
    }

    #endregion
  }
}