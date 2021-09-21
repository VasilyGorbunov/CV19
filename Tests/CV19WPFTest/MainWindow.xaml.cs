using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CV19WPFTest
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      new Thread(ComputeValue).Start();
    }

    private void ComputeValue()
    {
      var value = LongProcess(DateTime.Now);

      if(resultTextBlock.Dispatcher.CheckAccess())
        resultTextBlock.Text = value;
      else
      {
        //resultTextBlock.Dispatcher.Invoke(() => resultTextBlock.Text = value);
        resultTextBlock.Dispatcher.BeginInvoke(new Action(() => resultTextBlock.Text = value));
      }
    }

    private static string LongProcess(DateTime time)
    {
      Thread.Sleep(3000);
      return $"Result - {time}";
    }
  }
}
