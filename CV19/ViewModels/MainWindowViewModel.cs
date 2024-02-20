using CV19.Infrastructure.Commands.Base;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Group> Groups { get; }


        #region Properties



        #region SelectedGroup : Group - Выбранная группа студентов
        /// <summary>
        /// Выбранная группа студентов
        /// </summary>
        private Group _SelectedGroup;
        /// <summary>
        /// Выбранная группа студентов
        /// </summary>
        public Group SelectedGroup { get => _SelectedGroup; set => Set(ref _SelectedGroup, value); }
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

        #endregion

        public MainWindowViewModel()
        {
            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
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
                Birthday = DateTime.Now,
                Rating = 0
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            { 
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);
            #endregion
        }
    }
}
