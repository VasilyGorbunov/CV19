using System.Collections.Generic;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class CountriesStatisticViewModel: ViewModel
  {
    private readonly IDataService _dataService;
    

    #region Properties

    #region Статистика по странам
    private IEnumerable<CountryInfo> _countries;

    /// <summary>
    /// Статистика по странам
    /// </summary>
    public IEnumerable<CountryInfo> Countries
    {
      get => _countries;
      private set => Set(ref _countries, value);
    }
    #endregion

    #region Выбранная страна
    private CountryInfo _selectedCountry;
    /// <summary>
    /// Выбранная страна
    /// </summary>
    public CountryInfo SelectedCountry
    {
      get => _selectedCountry;
      set => Set(ref _selectedCountry, value);
    } 
    #endregion

    #endregion

    #region Commands
    public ICommand RefreshDataCommand { get; }

    private void OnRefreshDataCommandExecuted(object p)
    {
      Countries = _dataService.GetData();
    }
    #endregion

    /// <summary>
    /// Отладочный конструктор для визуального редактора
    /// </summary>
    //public CountriesStatisticViewModel() : this(null)
    //{
    //  if(!App.IsDesignMode)
    //    throw new InvalidOperationException("Вызов отладочного конструктора");

    //  _countries = Enumerable.Range(1, 10)
    //    .Select(i => new CountryInfo()
    //    {
    //      Name = $"Country {i}",
    //      Provinces = Enumerable.Range(1, 10).Select(j => new PlaceInfo()
    //      {
    //        Name = $"Province {j}",
    //        Location = new Point(i, j),
    //        Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount()
    //        {
    //          Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - j)),
    //          Count = k
    //        }).ToArray()
    //      }).ToArray()
    //    }).ToArray();
    //}

    public MainWindowViewModel MainModel { get; internal set; }
    public CountriesStatisticViewModel(IDataService dataService)
    {
      _dataService = dataService;

      RefreshDataCommand = new LambdaCommand(
        OnRefreshDataCommandExecuted);
    }
  }
}