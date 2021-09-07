using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class MainWindowViewModel: ViewModel
  {
    #region Свойства

    public ObservableCollection<Group> Groups { get; }

    #region Номер выбранной вкладки
    private int _selectedPageIndex = 0;
    /// <summary>
    /// Номер выбранной вкладки
    /// </summary>
    public int SelectedPageIndex
    {
      get => _selectedPageIndex;
      set => Set(ref _selectedPageIndex, value);
    } 
    #endregion

    #region Тестовый набор данных для визуализации графика

    private IEnumerable<DataPoint> _testDataPoints;
    /// <summary>
    /// Тестовый набор данных для визуализации графика
    /// </summary>
    public IEnumerable<DataPoint> TestDataPoints { 
      get => _testDataPoints; 
      set => Set(ref _testDataPoints, value);
    }

    #endregion

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

    #region ChangeTabIndexCommand - Смена tab вкладки
    public ICommand ChangeTabIndexCommand { get; set; }
    /// <summary>
    /// Смена tab вкладки
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private bool CanChangeTabIndexCommandExecute(object p) => SelectedPageIndex >= 0;

    private void OnChangeTabIndexCommandExecuted(object p)
    {
      if (p is null) return;
      SelectedPageIndex += Convert.ToInt32(p);
    } 
    #endregion

    #endregion

    #region Конструктор

    public MainWindowViewModel()
    {
      #region Студенты

      var student_index = 1;
      var students = Enumerable.Range(1, 25).Select(i => new Student
      {
        Name = $"Name {student_index}",
        Surname = $"Surname {student_index}",
        Patronymic = $"Patronymic {student_index++}",
        Birthday = DateTime.Now,
        Rating = 0
      });

      var groups = Enumerable.Range(1, 50).Select(i => new Group
      {
        Name = $"Группа №{i}",
        Students = new ObservableCollection<Student>(students)
      });
      Groups = new ObservableCollection<Group>(groups);
      #endregion

      #region Тестовый набор данных для визуализации графика
      var data_points = new List<DataPoint>((int) (360 / 0.1));
      for (var x = 0d; x <= 360; x += 0.1)
      {
        const double to_rad = Math.PI / 180;
        var y = Math.Sin(x * to_rad);
        data_points.Add(new DataPoint
        {
          XValue = x,
          YValue = y
        });
      }

      TestDataPoints = data_points;
      #endregion

      #region Команды

      CloseApplicationCommand = new LambdaCommand(
        OnCloseApplicationCommandExecuted, 
        CanCloseApplicationCommandExecute);

      ChangeTabIndexCommand = new LambdaCommand(
        OnChangeTabIndexCommandExecuted,
        CanChangeTabIndexCommandExecute);

      #endregion
    }

    #endregion
  }
}