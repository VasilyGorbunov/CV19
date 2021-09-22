using System;
using System.Threading;

namespace CV19Console
{
  class Program
  {
    static void Main()
    {
      Thread.CurrentThread.Name = "Main Thread";

      var thread = new Thread(ThreadMethod);
      thread.Name = "Other thread";
      thread.IsBackground = true;
      thread.Start();

      CheckThread();

      for (var i = 0; i < 100; i++)
      {
        Thread.Sleep(100);
        Console.WriteLine(i);
      }


      Console.ReadLine();
    }

    private static void ThreadMethod()
    {
      CheckThread();

      while (true)
      {
        Thread.Sleep(100);
        Console.Title = DateTime.Now.ToString();
      }
    }

    public static void CheckThread()
    {
      var thread = Thread.CurrentThread;
      Console.WriteLine($"{thread.ManagedThreadId}:{thread.Name}");
    }
  }
}
