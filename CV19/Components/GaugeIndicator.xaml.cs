using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CV19.Components
{
  /// <summary>
  /// Логика взаимодействия для GaugeIndicator.xaml
  /// </summary>
  public partial class GaugeIndicator : UserControl
  {
    #region Value Property
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
      nameof(Value),
      typeof(double),
      typeof(GaugeIndicator),
      new PropertyMetadata(
        default(double),
        OnValuePropertyChanged,
        OnCoerceValue),
      OnValidateValue);

    private static bool OnValidateValue(object value)
    {
      return true;
    }

    private static object OnCoerceValue(DependencyObject d, object basevalue)
    {
      var value = (double) basevalue;
      return Math.Max(0, Math.Min(100, value));
    }

    private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      //var gaugeIndicator = (GaugeIndicator) d;
      //gaugeIndicator.ArrowRotator.Angle = (double) e.NewValue;
    }

    [Category("My Category")]
    [Description("Индикатор топлива")]
    public double Value
    {
      get => (double)GetValue(ValueProperty);
      set => SetValue(ValueProperty, value);
    } 
    #endregion
    public GaugeIndicator()
    {
      InitializeComponent();
    }
  }
}
