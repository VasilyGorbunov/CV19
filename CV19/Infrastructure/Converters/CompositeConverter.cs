using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using CV19.Infrastructure.Converters.Base;

namespace CV19.Infrastructure.Converters
{
  [MarkupExtensionReturnType(typeof(CompositeConverter))]
  public class CompositeConverter: ConverterBase
  {
    [ConstructorArgument("First")]
    public IValueConverter First { get; set; }
    [ConstructorArgument("Second")]
    public IValueConverter Second { get; set; }

    public CompositeConverter() { }
    public CompositeConverter(IValueConverter first) => First = first;
    public CompositeConverter(IValueConverter first, IValueConverter second): this(first) => Second = second;

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