using System;
using System.ComponentModel;
using System.Windows;

namespace CV19.Views.Windows
{
  /// <summary>
  /// Логика взаимодействия для StudentEditorWindow.xaml
  /// </summary>
  public partial class StudentEditorWindow : Window
  {
    #region FirstName : string - Имя
    public static readonly DependencyProperty FirstNameProperty = DependencyProperty.Register(
      "FirstName", typeof(string), typeof(StudentEditorWindow), new PropertyMetadata(default(string)));

    [Description("Имя")]
    public string FirstName
    {
      get { return (string)GetValue(FirstNameProperty); }
      set { SetValue(FirstNameProperty, value); }
    } 
    #endregion

    #region LastName : string - Фамилия

    public static readonly DependencyProperty LastNameProperty = DependencyProperty.Register(
      "LastName", typeof(string), typeof(StudentEditorWindow), new PropertyMetadata(default(string)));

    [Description("Фамилия")]
    public string LastName
    {
      get { return (string) GetValue(LastNameProperty); }
      set { SetValue(LastNameProperty, value); }
    }

    #endregion

    #region Patronymic: string - Отчество
    public static readonly DependencyProperty PatronymicProperty = DependencyProperty.Register(
      "Patronymic", typeof(string), typeof(StudentEditorWindow), new PropertyMetadata(default(string)));

    [Description("Отчество")]
    public string Patronymic
    {
      get { return (string)GetValue(PatronymicProperty); }
      set { SetValue(PatronymicProperty, value); }
    }
    #endregion

    #region Rating : double - Рейтинг
    public static readonly DependencyProperty RatingProperty = DependencyProperty.Register(
      "Rating", typeof(double), typeof(StudentEditorWindow), new PropertyMetadata(default(double)));

    [Description("Рейтинг")]
    public double Rating
    {
      get { return (double)GetValue(RatingProperty); }
      set { SetValue(RatingProperty, value); }
    }
    #endregion

    #region Birthday : DateTime - Дата рождения
    public static readonly DependencyProperty BirthdayProperty = DependencyProperty.Register(
      "Birthday", typeof(DateTime), typeof(StudentEditorWindow), new PropertyMetadata(default(DateTime)));

    [Description("Дата рождения")]
    public DateTime Birthday
    {
      get { return (DateTime)GetValue(BirthdayProperty); }
      set { SetValue(BirthdayProperty, value); }
    } 
    #endregion

    public StudentEditorWindow() => InitializeComponent();
  }
}
