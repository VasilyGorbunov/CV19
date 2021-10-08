using System;
using System.Net;
using System.Threading.Tasks;

namespace CV19.Web
{
  

  public class WebServer
  {
    // private TcpListener _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));
    public event EventHandler<RequestReceiverEventArgs> _requestReceived;

    private HttpListener _listener;
    private readonly int _port;
    private bool _enabled;
    private readonly object _syncRoot = new object();

    public WebServer(int port) => _port = port;

    public int Port => _port;
    public bool Enabled
    {
      get => _enabled;
      set
      {
        if (value)
          Start();
        else
          Stop();
      }


    }

    public void Start()
    {
      if (_enabled) return;

      lock (_syncRoot)
      {
        if (_enabled) return;

        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://*:{_port}/"); // netsh http add urlacl url = http://+:8088/ user=vasily
        _listener.Prefixes.Add($"http://+:{_port}/");
        _enabled = true;
        ListenAsync();
      }
      

    }

    public void Stop()
    {
      if (!_enabled) return;

      lock (_syncRoot)
      {
        if (!_enabled) return;

        _listener = null;
        _enabled = false;
      }
      
    }

    private async void ListenAsync()
    {
      var listener = _listener;
      listener.Start();

      HttpListenerContext context = null;

      while (_enabled)
      {
        var getContextTask = listener.GetContextAsync();
        if (context != null)
          ProcessRequest(context);
        context = await getContextTask.ConfigureAwait(false);
      }

      listener.Stop();
    }

    private void ProcessRequest(HttpListenerContext context)
    {
      _requestReceived?.Invoke(this, new RequestReceiverEventArgs(context));
    }
  }

  public class RequestReceiverEventArgs : EventArgs
  {
    public RequestReceiverEventArgs(HttpListenerContext context)
    {
      Context = context;
    }

    public  HttpListenerContext Context { get; }
  }
}