using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using CV19.Infrastructure.Converters.Base;

namespace CV19.Infrastructure.Converters
{
  [MarkupExtensionReturnType(typeof(RatioConverter))]
  public class RatioConverter: ConverterBase
  {
    [ConstructorArgument("K")]
    public double K { get; set; } = 1;

    public RatioConverter() { }

    public RatioConverter(double K) => this.K = K;

    public override object Convert(object v, Type t, object p, CultureInfo c)
    {
      if (v is null) return null;
      var x = System.Convert.ToDouble(v, c);

      return x * K;
    }
  }
}
