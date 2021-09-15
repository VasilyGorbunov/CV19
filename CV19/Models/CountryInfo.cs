using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models
{
  public class CountryInfo : PlaceInfo
  {
    private Point? _location;

    public override Point Location
    {
      get
      {
        if (_location != null)
          return (Point) _location;

        if (Provinces is null)
          return default;

        var average_x = Provinces.Average(p => p.Location.X);
        var average_y = Provinces.Average(p => p.Location.Y);

        return (Point)(_location = new Point(average_x, average_y));
      }

      set => _location = value;
    }
    public IEnumerable<PlaceInfo> Provinces { get; set; }

    private IEnumerable<ConfirmedCount> _counts;

    public override IEnumerable<ConfirmedCount> Counts
    {
      get
      {
        if (_counts != null) return _counts;
        var pointsCount = Provinces.FirstOrDefault()?.Counts?.Count() ?? 0;

        if (pointsCount == 0) return Enumerable.Empty<ConfirmedCount>();

        var provincePoints = Provinces.Select(p => p.Counts.ToArray()).ToArray();

        var points = new ConfirmedCount[pointsCount];

        foreach (var province in provincePoints)
          for (var i = 0; i < pointsCount; i++)
          {
            if (points[i].Date == default)
              points[i] = province[i];
            else
              points[i].Count += province[i].Count;

          }

        return points;
      }
      set => _counts = value;
    }
  }
}