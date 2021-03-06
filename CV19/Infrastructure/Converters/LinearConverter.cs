using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using CV19.Infrastructure.Converters.Base;

namespace CV19.Infrastructure.Converters
{
  [ValueConversion(typeof(double), typeof(double))]
  [MarkupExtensionReturnType(typeof(LinearConverter))]
  public class LinearConverter: ConverterBase
  {
    [ConstructorArgument("K")]
    public double K { get; set; } = 1;

    [ConstructorArgument("B")]
    public double B { get; set; }

    public LinearConverter() {}

    public LinearConverter(double K): this() => this.K = K;

    public LinearConverter(double K, double B): this(K) => this.B = B;

    public override object Convert(object v, Type t, object p, CultureInfo c)
    {
      if (v is null) return null;
      var x = System.Convert.ToDouble(v, c);
      return K * x + B;
    }

    public override object ConvertBack(object v, Type t, object p, CultureInfo c)
    {
      if (v is null) return null;
      var y = System.Convert.ToDouble(v, c);

      return (y - B) / K;
    }
  }
}