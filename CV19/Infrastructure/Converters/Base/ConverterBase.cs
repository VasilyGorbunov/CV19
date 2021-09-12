using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters.Base
{
  public abstract class ConverterBase: IValueConverter
  {
    public abstract object Convert(object v, Type t, object p, CultureInfo c);

    public virtual object ConvertBack(object v, Type t, object p, CultureInfo c) =>
      throw new NotSupportedException("Обратное преобразование не поддерживается");

  }
}