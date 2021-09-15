using System;
using System.Globalization;

namespace CV19.Infrastructure.Converters
{
  public class ToArrayConverter: MultiConverter
  {
    public override object Convert(object[] vv, Type t, object p, CultureInfo c) => vv;

    // public override object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c) => v as object[];
  }
}