namespace CV19.Services.Interfaces
{
  public interface IUserDialogService
  {
    bool Edit(object item);

    void ShowInformation(string information, string caption);

    void ShowWarning(string message, string caption);

    void ShowError(string message, string caption);

    bool Confirm(string message, string caption, bool exclamation = false);

    string GetStringValue(string message, string caption, string defaultValue = null);
  }
}