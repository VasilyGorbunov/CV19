using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
  public class MainWindowViewModel : ViewModel
  {

    #region Свойства

    public ObservableCollection<Group> Groups { get; }

    public object[] CompositeCollection { get; }

    #region Выбранный непонятный элемент

    private object _selectedCompositeValue;

    /// <summary>
    /// Выбранный непонятный элемент
    /// </summary>
    public object SelectedCompositeValue
    {
      get => _selectedCompositeValue;
      set => Set(ref _selectedCompositeValue, value);
    }

    #endregion

    #region Выбранная группа
    private Group _selectedGroup;
    /// <summary>
    /// Выбранная группа
    /// </summary>
    public Group SelectedGroup
    {
      get => _selectedGroup;
      set
      {
        if (!Set(ref _selectedGroup, value)) return;
        _selectedGroupStudents.Source = value?.Students;
        OnPropertyChanged(nameof(SelectedGroupStudents));
      }
    }
    #endregion

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
    public IEnumerable<DataPoint> TestDataPoints
    {
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

    #region Виртуализация

    public IEnumerable<Student> TestStudents => Enumerable
      .Range(1, App.IsDesignMode ? 10 : 100_000)
      .Select(i => new Student
      {
        Name = $"Имя {i}",
        Surname = $"Фамилия {i}",
        Patronymic = $"Отчество {i}",
      });
    #endregion

    #region Текст фильтра студентов
    private string _studentFilterText;
    /// <summary>
    /// Текст фильтра студентов
    /// </summary>
    public string StudentFilterText
    {
      get => _studentFilterText;
      set
      {
        if (!Set(ref _studentFilterText, value)) return;
        _selectedGroupStudents.View.Refresh();
      }
    }

    #endregion

    #region Фильтрация студентов
    private readonly CollectionViewSource _selectedGroupStudents = new CollectionViewSource();

    public ICollectionView SelectedGroupStudents => _selectedGroupStudents?.View;

    private void OnStudentsFilter(object sender, FilterEventArgs e)
    {
      if (!(e.Item is Student student)) return;
      if (student.Name is null) return;

      var filter_text = _studentFilterText;
      if (string.IsNullOrWhiteSpace(filter_text)) return;

      if (student.Name is null || student.Surname is null)
      {
        e.Accepted = false;
        return;
      }

      if (student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
      if (student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

      e.Accepted = false;
    }
    #endregion

    #region Результат длительной асинхронной операции

    private string _dataValue;

    /// <summary>
    /// Результат длительной асинхронной операции
    /// </summary>
    public string DataValue
    {
      get => _dataValue;
      private set => Set(ref _dataValue, value);
    }

    #endregion

    public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("Z:\\");

    #region Выбранная директория
    private DirectoryViewModel _selectedDirectory;
    /// <summary>
    /// Выбранная директория
    /// </summary>
    public DirectoryViewModel SelectedDirectory
    {
      get => _selectedDirectory;
      set => Set(ref _selectedDirectory, value);
    }
    #endregion

    #region FuelCount : double - Указатель топлива

    /// <summary>Указатель топлива</summary>
    private double _fuelCount;

    /// <summary>Указатель топлива</summary>
    public double FuelCount
    {
      get => _fuelCount;
      set => Set(ref _fuelCount, value);
    }

    #endregion

    #region Coefficient : double - Коэффициент

    /// <summary>Коэффициент</summary>
    private double _сoefficient = 1;

    /// <summary>Коэффициент</summary>
    public double Coefficient
    {
      get => _сoefficient;
      set => Set(ref _сoefficient, value);
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
      (RootObject as Window)?.Close();
      //Application.Current.Shutdown();
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

    #region CreateGroupCommand - Создание группы студентов
    public ICommand CreateGroupCommand { get; }

    private bool CanCreateGroupCommandExecute(object p) => true;

    private void OnCreateGroupCommandExecuted(object p)
    {
      var group_max_index = Groups.Count + 1;

      var new_group = new Group
      {
        Name = $"Группа №{group_max_index}",
        Students = new ObservableCollection<Student>()
      };

      Groups.Add(new_group);
    }
    #endregion

    #region DeleteGroupCommand - Удаление группы студентов
    public ICommand DeleteGroupCommand { get; }
    /// <summary>
    /// Удаление группы студентов
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);

    private void OnDeleteGroupCommandExecuted(object p)
    {
      if (!(p is Group group)) return;
      var group_index = Groups.IndexOf(group);
      Groups.Remove(group);

      if (group_index < Groups.Count)
        SelectedGroup = Groups[group_index];

    }
    #endregion

    #region Command StartProcessCommand - Запуск процесса

    /// <summary>Запуск процесса</summary>
    public ICommand StartProcessCommand { get; }

    private static bool CanStartProcessCommandExecute(object p) => true;

    private void OnStartProcessCommandExecuted(object p)
    {
      new Thread(ComputeValue).Start();
    }

    private void ComputeValue()
    {
      DataValue = _asyncData.GetResult(DateTime.Now);
    }

    #endregion

    #region Command StopProcessCommand - Остановка процесса

    /// <summary>Остановка процесса</summary>
    public ICommand StopProcessCommand { get; }

    // Проверка возможности выполнения
    private static bool CanStopProcessCommandExecute(object p) => true;

    // Логика выполнения
    private void OnStopProcessCommandExecuted(object p)
    {

    }

    #endregion


    #endregion

    #region Конструктор

    public CountriesStatisticViewModel CountriesStatistic { get; }
    private readonly IAsyncDataService _asyncData;
    public WebServerViewModel WebServer { get; }

    public MainWindowViewModel(
      CountriesStatisticViewModel statistic, 
      IAsyncDataService asyncData,
      WebServerViewModel webServer)
    {
      CountriesStatistic = statistic;
      WebServer = webServer;
      _asyncData = asyncData;
      statistic.MainModel = this;

      #region Студенты

      var student_index = 1;
      var students = Enumerable.Range(1, 10).Select(i => new Student
      {
        Name = $"Name {student_index}",
        Surname = $"Surname {student_index}",
        Patronymic = $"Patronymic {student_index++}",
        Birthday = DateTime.Now,
        Rating = 0
      });

      var groups = Enumerable.Range(1, 5).Select(i => new Group
      {
        Name = $"Группа № {i}",
        Students = new ObservableCollection<Student>(students)
      });
      Groups = new ObservableCollection<Group>(groups);
      #endregion

      #region Тестовый набор данных для визуализации графика
      var data_points = new List<DataPoint>((int)(360 / 0.1));
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

      #region Composite Collection
      var data_list = new List<object>();
      data_list.Add("Hello world");
      data_list.Add(42);
      var group = Groups[1];
      data_list.Add(group);
      data_list.Add(group.Students[0]);

      CompositeCollection = data_list.ToArray();
      #endregion

      #region Фильтрация, сортировка студентов
      _selectedGroupStudents.Filter += OnStudentsFilter;
      _selectedGroupStudents.SortDescriptions.Add(
        new SortDescription("Name", ListSortDirection.Descending));
      _selectedGroupStudents.GroupDescriptions.Add(
        new PropertyGroupDescription("Name"));
      #endregion

      #region Команды

      CloseApplicationCommand = new LambdaCommand(
        OnCloseApplicationCommandExecuted,
        CanCloseApplicationCommandExecute);

      ChangeTabIndexCommand = new LambdaCommand(
        OnChangeTabIndexCommandExecuted,
        CanChangeTabIndexCommandExecute);

      CreateGroupCommand = new LambdaCommand(
        OnCreateGroupCommandExecuted,
        CanCreateGroupCommandExecute);

      DeleteGroupCommand = new LambdaCommand(
        OnDeleteGroupCommandExecuted,
        CanDeleteGroupCommandExecute);

      StartProcessCommand = new LambdaCommand(
        OnStartProcessCommandExecuted,
        CanStartProcessCommandExecute);

      StopProcessCommand = new LambdaCommand(
        OnStopProcessCommandExecuted,
        CanStopProcessCommandExecute);

      #endregion
    }



    #endregion
  }
}