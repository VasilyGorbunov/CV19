﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CV19.ViewModels.Base;

internal abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if(Equals(field, value)) return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing ||  _disposed) return;
        _disposed = true;
        // Освобождение управляемых ресурсов
    }

    public void Dispose()
    {
        Dispose(true);
    }
}
