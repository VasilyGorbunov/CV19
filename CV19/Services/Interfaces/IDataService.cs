using System.Collections.Generic;
using CV19.Models;

namespace CV19.Services.Interfaces
{
  public interface IDataService
  {
    IEnumerable<CountryInfo> GetData();
  }
}