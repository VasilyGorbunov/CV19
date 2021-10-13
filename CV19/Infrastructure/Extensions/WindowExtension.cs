using System.Runtime.InteropServices;
using System.Windows.Interop;
using CV19.Infrastructure.Extensions;

namespace System.Windows
{
  public static class WindowExtension
  {
    private const string user32 = "user32.dll";

    [DllImport(user32, CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    public static IntPtr SendMessage(this Window window, WM Msg, SC wParam, IntPtr lParam = default) => 
      SendMessage(window.GetWindowHandle(), (uint)Msg, (IntPtr)wParam, lParam == default ? (IntPtr)' ' : lParam);

    public static IntPtr GetWindowHandle(this Window window)
    {
      var helper = new WindowInteropHelper(window);
      return helper.Handle;
    }
  }
}