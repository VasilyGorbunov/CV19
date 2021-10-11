using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace CV19.Infrastructure.Behaviors
{
  public class MinimizeWindow : Behavior<Button>
  {
    protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

    protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
      if (!(AssociatedObject.FindVisualRoot() is Window window)) return;

      window.WindowState = WindowState.Minimized;
    }
  }
}