using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace CV19Console
{
  class Program
  {
    private static bool _threadUpdate = true;
    static void Main()
    {
      Thread.CurrentThread.Name = "Main Thread";

      var clockThread = new Thread(ThreadMethod);
      clockThread.Name = "Other thread";
      clockThread.IsBackground = true;
      clockThread.Start(42);

      //var count = 5;
      //var msg = "Hello Thread";
      //var tm = 150;

      //new Thread(() => PrintMethod(msg, count, tm)) {IsBackground = true}.Start();

      //CheckThread();

      //foreach (var thread in threads)
      //{
      //  thread.Start();
      //}

      var values = new List<int>();
      var threads = new Thread[10];
      object object_lock = new object();

      for (int i = 0; i < threads.Length; i++)
      {
        threads[i] = new Thread(() =>
        {
          for (int j = 0; j < 10; j++)
          {
            lock (object_lock)
            {
              values.Add(Thread.CurrentThread.ManagedThreadId);
            }
            Thread.Sleep(1);
          }
        });
      }

      foreach (var thread in threads)
      {
        thread.Start();
      }


      if(!clockThread.Join(200))
        clockThread.Interrupt();

      Mutex mutex = new Mutex();

      Console.WriteLine(string.Join(",", values));
      Console.ReadLine();
    }

    private static void PrintMethod(string message, int count, int timeout)
    {
      for (int i = 0; i < count; i++)
      {
        Console.WriteLine(message);
        Thread.Sleep(timeout);
      }
    }

    private static void ThreadMethod(object parameter)
    {
      var value = (int) parameter;
      Console.WriteLine(value);
      CheckThread();

      while (_threadUpdate)
      {
        Thread.Sleep(100);
        Console.Title = DateTime.Now.ToString(CultureInfo.InvariantCulture);
      }
    }

    public static void CheckThread()
    {
      var thread = Thread.CurrentThread;
      Console.WriteLine($"{thread.ManagedThreadId}:{thread.Name}");
    }
  }
}
