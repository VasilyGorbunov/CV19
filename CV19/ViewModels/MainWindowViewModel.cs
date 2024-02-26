using CV19.Infrastructure.Commands.Base;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Group> Groups { get; }
        public object[] CompositeCollection { get; }



        #region Properties



        #region SelectedCompositeValue : object - Выбранное композитное значение
        /// <summary>
        /// Выбранное композитное значение
        /// </summary>
        private object _SelectedCompositeValue;
        /// <summary>
        /// Выбранное композитное значение
        /// </summary>
        public object SelectedCompositeValue { get => _SelectedCompositeValue; set => Set(ref _SelectedCompositeValue, value); }
        #endregion


        #region SelectedGroup : Group - Выбранная группа студентов
        /// <summary>
        /// Выбранная группа студентов
        /// </summary>
        private Group _SelectedGroup;
        /// <summary>
        /// Выбранная группа студентов
        /// </summary>
        public Group SelectedGroup { get => _SelectedGroup;
            set
            {
                if (!Set(ref _SelectedGroup, value)) return;
                _SelectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
            }
        }
        #endregion

        #region Текст фильтрации студентов

        private string _StudentFilterText;

        /// <summary>
        /// Текст фильтрации студентов
        /// </summary>
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            }
        }

        #endregion

        #region SelectedGroupStudent
        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;

        private void OnStudentsFiltered(object sender, FilterEventArgs e)
        {
            if(!(e.Item is Student student)) return;

            var filter_text = _StudentFilterText;
            if(string.IsNullOrWhiteSpace(filter_text)) return;

            if (student.Name is null || student.Surname is null)
            {
                e.Accepted = false;
                return;
            }

            if(student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if(student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;

        } 
        #endregion


        #region SelectedPageIndex : int - Выбранная вкладка в TabControl
        /// <summary>
        /// Выбранная вкладка в TabControl
        /// </summary>
        private int _SelectedPageIndex = 2;
        /// <summary>
        /// Выбранная вкладка в TabControl
        /// </summary>
        public int SelectedPageIndex { get => _SelectedPageIndex; set => Set(ref _SelectedPageIndex, value); }
        #endregion


        #region TestDataPoints : IEnumerable<DataPoint> - Тестовые данные для графика
        /// <summary>
        /// Тестовые данные для графика
        /// </summary>
        private IEnumerable<DataPoint> _TestDataPoints;
        /// <summary>
        /// Тестовые данные для графика
        /// </summary>
        public IEnumerable<DataPoint> TestDataPoints { get => _TestDataPoints; set => Set(ref _TestDataPoints, value); }
        #endregion


        #region Title : string - Заголовок окна
        /// <summary>
        /// Заголовок окна
        /// </summary>
        private string _Title = "Анализ статистики CV19";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region Status : string - Статус программы
        /// <summary>Статус программы</summary>
        private string _Status = "Готов!";
        /// <summary>Статус программы</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }
        #endregion

        public IEnumerable<Student> TestStudents => Enumerable.Range(1, App.IsDesignMode ? 10 : 100_000)
            .Select(x => new Student
            {
                Name = $"Имя {x}",
                Surname = $"Фамилия {x}"
            });

        #endregion

        #region Commands

        #region Close Application Command
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p) 
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Change TabIndex Command
        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object p) => SelectedPageIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }
        #endregion

        #region CreateGroupCommand - создание новой группы
        public ICommand CreateGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;

        private void OnCreateGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group()
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()

            };

            Groups.Add(new_group);
        } 
        #endregion

        #region DeleteGroupCommand - удаление группы
        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);

        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (p is not Group group) return;

            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
                SelectedGroup = Groups[group_index];
        } 
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);
            #endregion

            #region Генерация тестовых данных для графика

            var data_points = new List<DataPoint>((int)(360 / 0.1));

            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);
                data_points.Add(new DataPoint { XValue = x, YValue = y});
            }

            TestDataPoints = data_points;

            #endregion

            #region Студенты и группы
            int student_index = 1;
            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index}",
                Description = $"Description {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0,
                
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            { 
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students),
                Description = $"Description {i}"
            });

            Groups = new ObservableCollection<Group>(groups);
            #endregion

            #region Composite Collection
            var data_list = new List<object>();
            data_list.Add("Hello world!");
            data_list.Add(42);

            var group = Groups[1];
            data_list.Add(group);
            data_list.Add(group.Students[0]);

            CompositeCollection = data_list.ToArray();
            #endregion

            #region фильтрация студентов

            _SelectedGroupStudents.Filter += OnStudentsFiltered;
            //_SelectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            //_SelectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));

            #endregion
        }

        
    }
}
