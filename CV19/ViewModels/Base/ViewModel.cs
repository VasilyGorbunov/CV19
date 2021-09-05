#nullable enable
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CV19.ViewModels.Base
{
  public abstract class ViewModel: INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Кольцевое обновление свойства
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <param name="propertyName"></param>
    /// <returns>bool</returns>
    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
      if (Equals(field, value)) return false; // выходим, если значение не изменилось

      field = value;
      OnPropertyChanged(propertyName);
      return true;
    }
  }
}