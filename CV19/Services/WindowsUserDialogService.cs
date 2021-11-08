using System;
using System.Linq;
using System.Windows;
using CV19.Models.Decanat;
using CV19.Services.Interfaces;
using CV19.Views.Windows;

namespace CV19.Services
{
  public class WindowsUserDialogService : IUserDialogService
  {
    private static Window _activeWindow => Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
    public bool Edit(object item)
    {
      if(item is null)
        throw new ArgumentNullException(nameof(item));

      switch (item)
      {
        case Student student:
          return EditStudent(student);
        default:
          throw new NotSupportedException($"Редактирование объекта типа {item.GetType().Name} не поддерживается");
      }
    }

    public void ShowInformation(string information, string caption) =>
      MessageBox.Show(
        information,
        caption,
        MessageBoxButton.OK,
        MessageBoxImage.Information);

    public void ShowWarning(string message, string caption) =>
      MessageBox.Show(
        message,
        caption,
        MessageBoxButton.OK,
        MessageBoxImage.Warning);

    public void ShowError(string message, string caption) =>
      MessageBox.Show(
        message,
        caption,
        MessageBoxButton.OK,
        MessageBoxImage.Error);

    public bool Confirm(string message, string caption, bool exclamation = false) =>
      MessageBox.Show(
        message, 
        caption, 
        MessageBoxButton.YesNo, 
        exclamation ? MessageBoxImage.Exclamation : MessageBoxImage.Question
        ) == MessageBoxResult.Yes;

    public string GetStringValue(string message, string caption, string defaultValue = null)
    {
      
      var value_dialog = new StringValueDialogWindow
      {
        Message = message,
        Title = caption,
        Value = defaultValue ?? string.Empty,
        Owner = _activeWindow,
        WindowStartupLocation = WindowStartupLocation.CenterOwner
      };

      return value_dialog.ShowDialog() == true ? value_dialog.Value : defaultValue;
    }

    private static bool EditStudent(Student student)
    {
      var dlg = new StudentEditorWindow
      {
        FirstName = student.Name,
        LastName = student.Surname,
        Patronymic = student.Patronymic,
        Rating = student.Rating,
        Birthday = student.Birthday,
        Owner = _activeWindow,
        WindowStartupLocation = WindowStartupLocation.CenterOwner
      };

      if (dlg.ShowDialog() != true)
        return false;

      student.Name = dlg.FirstName;
      student.Surname = dlg.LastName;
      student.Patronymic = dlg.Patronymic;
      student.Rating = dlg.Rating;
      student.Birthday = dlg.Birthday;

      return true;
    }
  }
}