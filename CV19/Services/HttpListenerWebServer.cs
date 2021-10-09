using System;
using System.IO;
using CV19.Services.Interfaces;
using CV19.Web;

namespace CV19.Services
{
  public class HttpListenerWebServer: IWebServerService
  {
    private readonly WebServer _server = new WebServer(8080);
    public bool Enabled { get => _server.Enabled; set => _server.Enabled = value; }
    public void Start() => _server.Start();

    public void Stop() => _server.Stop();

    public HttpListenerWebServer() => _server._requestReceived += OnRequestReceived;

    private void OnRequestReceived(object sender, RequestReceiverEventArgs e)
    {
      using var writer = new StreamWriter(e.Context.Response.OutputStream);
      writer.WriteLine("CV19 Application - " + DateTime.Now);
    }
  }
}