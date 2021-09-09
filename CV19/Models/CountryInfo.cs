using System.Collections.Generic;

namespace CV19.Models
{
  public class CountryInfo : PlaceInfo
  {
    public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
  }
}