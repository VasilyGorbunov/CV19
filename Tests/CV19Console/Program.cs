using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CV19Console
{
  class Program
  {
    static void  Main(string[] args)
    {
      Thread.CurrentThread.Name = "Main Thread";

      var thread = new Thread(ThreadMethod);
      thread.Name = "Other thread";
      thread.Start();

      CheckThread();

      Console.ReadLine();
    }

    private static void ThreadMethod()
    {
      CheckThread();
    }

    public static void CheckThread()
    {
      var thread = Thread.CurrentThread;
      Console.WriteLine($"{thread.ManagedThreadId}:{thread.Name}");
    }
  }
}
