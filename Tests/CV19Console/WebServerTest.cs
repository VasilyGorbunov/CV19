using System;
using System.IO;
using CV19.Web;

namespace CV19Console
{
  public static class WebServerTest
  {
    public static void Run()
    {
      var server = new WebServer(8080);
      server._requestReceived += OnRequestReceived;

      server.Start();
      Console.WriteLine("Server running!");
      Console.ReadLine();
    }

    private static void OnRequestReceived(object? sender, RequestReceiverEventArgs e)
    {
      var context = e.Context;

      Console.WriteLine($"Connection - {context.Request.UserHostAddress}");

      using var writer = new StreamWriter(context.Response.OutputStream);
      writer.WriteLine("Hello from Test Web Server");
    }
  }
}