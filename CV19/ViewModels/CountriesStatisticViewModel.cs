using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
  public class CountriesStatisticViewModel: ViewModel
  {
    private DataService _dataService;
    private MainWindowViewModel MainModel { get; }

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
    public CountriesStatisticViewModel() : this(null)
    {
      if(!App.IsDedignMode)
        throw new InvalidOperationException("Вызов отладочного конструктора");

      _countries = Enumerable.Range(1, 10)
        .Select(i => new CountryInfo()
        {
          Name = $"Country {i}",
          ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo()
          {
            Name = $"Province {j}",
            Location = new Point(i, j),
            Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount()
            {
              Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - j)),
              Count = k
            }).ToArray()
          }).ToArray()
        }).ToArray();
    }

    public CountriesStatisticViewModel(MainWindowViewModel mainModel)
    {
      MainModel = mainModel;
      _dataService = new DataService();

      RefreshDataCommand = new LambdaCommand(
        OnRefreshDataCommandExecuted);
    }
  }
}