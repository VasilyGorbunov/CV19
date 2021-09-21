using System;
using System.Diagnostics;
using System.Globalization;
using CV19.Infrastructure.Converters.Base;

namespace CV19.Infrastructure.Converters
{
  public class DebugConverter: ConverterBase
  {
    public override object Convert(object v, Type t, object p, CultureInfo c)
    {
      Debugger.Break();

      return v;
    }

    public override object ConvertBack(object v, Type t, object p, CultureInfo c)
    {
      Debugger.Break();

      return v;
    }
  }
}