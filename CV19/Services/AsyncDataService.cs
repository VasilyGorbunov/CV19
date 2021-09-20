using System;
using System.Threading;
using CV19.Services.Interfaces;

namespace CV19.Services
{
  public class AsyncDataService: IAsyncDataService
  {
    private const int _sleepTime = 7000;

    public string GetResult(DateTime time)
    {
      Thread.Sleep(_sleepTime);

      return $"Result value {time}";
    }
  }
}