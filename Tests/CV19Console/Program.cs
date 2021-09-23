using System;
using System.Collections.Generic;
using System.Threading;

namespace CV19Console
{
  class Program
  {
    static void Main()
    {
      Thread.CurrentThread.Name = "Main Thread";

      //var thread = new Thread(ThreadMethod);
      //thread.Name = "Other thread";
      //thread.IsBackground = true;
      //thread.Start(42);

      //var count = 5;
      //var msg = "Hello Thread";
      //var tm = 150;

      //new Thread(() => PrintMethod(msg, count, tm)) {IsBackground = true}.Start();

      //CheckThread();

      //for (var i = 0; i < 10; i++)
      //{
      //  Thread.Sleep(100);
      //  Console.WriteLine(i);
      //}

      var values =   new List<int>();
      var threads = new Thread[10];
      for (int i = 0; i < threads.Length; i++)
      {
        threads[i] = new Thread(() =>
        {
          for(int j = 0; j < 10; j++)
            values.Add(Thread.CurrentThread.ManagedThreadId);
        });
      }

      foreach (var thread in threads)
      {
        thread.Start();
      }


      Console.ReadLine();
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
