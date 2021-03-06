#nullable enable
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xaml;

namespace CV19.ViewModels.Base
{
  public abstract class ViewModel: MarkupExtension, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      var handlers = PropertyChanged;
      if (handlers is null) return;

      var invocationList = handlers.GetInvocationList();
      var arg = new PropertyChangedEventArgs(propertyName);

      foreach (var action in invocationList)
      {
        if (action.Target is DispatcherObject dispObject)
          dispObject.Dispatcher.Invoke(action, this, arg);
        else
          action.DynamicInvoke(this, arg);
      }
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

    public override object ProvideValue(IServiceProvider sp)
    {
      var valueTargetService = sp.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
      var rootObjectService = sp.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;

      OnInitialized(valueTargetService?.TargetObject, 
        valueTargetService?.TargetProperty, 
        rootObjectService?.RootObject);

      return this;
    }

    private WeakReference _targetRef;
    private WeakReference _rootRef;

    public object TargetObject => _targetRef?.Target;
    public object RootObject => _rootRef?.Target;

    protected virtual void OnInitialized(object target, object property, object root)
    {
      _targetRef = new WeakReference(target);
      _rootRef = new WeakReference(root);
    }
  }
}