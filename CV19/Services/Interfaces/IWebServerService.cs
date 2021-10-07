namespace CV19.Services.Interfaces
{
  public interface IWebServerService
  {
    bool Enabled { get; set; }

    void Start();

    void Stop();
  }
}