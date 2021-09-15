using System;
using System.Globalization;
using System.Windows.Data;
using CV19.Infrastructure.Converters.Base;

namespace CV19.Infrastructure.Converters
{
  public abstract class MultiConverter: IMultiValueConverter
  {
    public abstract object Convert(object[] vv, Type t, object p, CultureInfo c);

    public virtual object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c) => 
      throw new NotSupportedException("Не поддерживается.");
  }
}