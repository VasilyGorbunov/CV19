using System;
using System.Linq;
using System.Windows.Markup;

namespace CV19.Infrastructure.Common
{
  [MarkupExtensionReturnType(typeof(int[]))]
  public class StringToIntArray: MarkupExtension
  {
    [ConstructorArgument("Str")]
    public string Str { get; set; }

    public char Separator { get; set; } = ';';

    public StringToIntArray() {}

    public StringToIntArray(string Str) => this.Str = Str;

    public override object ProvideValue(IServiceProvider sp) =>
      Str.Split(new[] {Separator}, StringSplitOptions.RemoveEmptyEntries)
        .DefaultIfEmpty()
        .Select(int.Parse)
        .ToArray();
  }
}