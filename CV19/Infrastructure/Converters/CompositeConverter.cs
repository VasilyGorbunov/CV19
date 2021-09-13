using System;
using System.Globalization;
using System.Windows.Data;
using CV19.Infrastructure.Converters.Base;

namespace CV19.Infrastructure.Converters
{
  public class CompositeConverter: ConverterBase
  {

    public IValueConverter First { get; set; }
    public IValueConverter Second { get; set; }

    public override object Convert(object v, Type t, object p, CultureInfo c)
    {
      var result1 = First?.Convert(v, t, p, c) ?? v;
      var result2 = Second?.Convert(v, t, p, c) ?? v;

      return result2;
    }

    public override object ConvertBack(object v, Type t, object p, CultureInfo c)
    {
      var result2 = Second?.ConvertBack(v, t, p, c) ?? v;
      var result1 = First?.ConvertBack(v, t, p, c) ?? v;

      return result1;

    }
  }
}