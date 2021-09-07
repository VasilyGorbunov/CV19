using System.Collections.Generic;

namespace CV19.Models
{
  public class CountryInfo : PlaceInfo
  {
    public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
  }
}