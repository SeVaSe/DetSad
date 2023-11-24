using DetSad.AdditPages;
using DetSad.Classes;
using DetSad.DateBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DetSad.Pages.TeacherPages
{
    /// <summary>
    /// Логика взаимодействия для MyGroupPage.xaml
    /// </summary>
    public partial class MyGroupPage : Page
    {
        public MyGroupPage()
        {
            InitializeComponent();
            Loaded += GroupPage_Loaded;
        }

        public int GetTeacherIDByLogin(string username)
        {
            using (var context = new KindergartenDBEntities())
            {
                var teacher = context.Users.FirstOrDefault(u => u.Username == username && u.Role == "teacher");
                if (teacher != null)
                {
                    return teacher.UserID; // Возвращает ID учителя, если он найден
                }
                return 0; // Возвращаем значение по умолчанию, если учителя не найдено
            }
        }

        public int GetTeacherGroupID(int teacherID)
        {
            using (var context = new KindergartenDBEntities())
            {
                var teacher = context.Users.FirstOrDefault(u => u.UserID == teacherID && u.Role == "teacher");
                if (teacher != null)
                {
                    return teacher.GroupID ?? 0; // Возвращает ID группы учителя, если он есть, или 0
                }
                return 0; // Возвращаем значение по умолчанию, если что-то пошло не так
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                StudentModel student = checkBox.DataContext as StudentModel;
                if (student != null)
                {
                    // Получаем студента по его ФИО (или уникальному идентификатору) и обновляем IsHere
                    UpdateIsHereInDatabase(student.FIO, true); // Обновление значения в базе данных
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                StudentModel student = checkBox.DataContext as StudentModel;
                if (student != null)
                {
                    // Получаем студента по его ФИО (или уникальному идентификатору) и обновляем IsHere
                    UpdateIsHereInDatabase(student.FIO, false); // Обновление значения в базе данных
                }
            }
        }

        // Метод для обновления значения IsHere в базе данных
        public void UpdateIsHereInDatabase(string studentFIO, bool isHere)
        {
            using (var context = new KindergartenDBEntities())
            {
                var student = context.Children.FirstOrDefault(s => s.ChildName == studentFIO);
                if (student != null)
                {
                    student.IsHere = isHere;
                    context.SaveChanges(); // Сохраняем изменения в базе данных
                }
            }
        }



        private void GroupPage_Loaded(object sender, RoutedEventArgs e)
        {
            string username = InfoUserControl.GetLogin(); // Получаем логин учителя

            int teacherID = GetTeacherIDByLogin(username); // Получаем ID учителя
            int groupID = GetTeacherGroupID(teacherID); // Получаем ID группы учителя

            // Получаем список студентов для данной группы учителя
            List<StudentModel> students = GetStudentsByGroupID(groupID);

            // Загружаем студентов в DataGrid
            EventsDataGrid.ItemsSource = students;
        }

        // Метод, который получает список студентов для данной группы
        public List<StudentModel> GetStudentsByGroupID(int groupID)
        {
            using (var context = new KindergartenDBEntities())
            {
                var students = context.Children.Where(s => s.GroupID == groupID).ToList();

                // Преобразуйте полученные данные из базы данных в объекты StudentModel
                List<StudentModel> studentModels = students.Select(s => new StudentModel
                {
                    FIO = s.ChildName,
                    IsHere = s.IsHere ?? false // Если IsHere == null, устанавливаем значение по умолчанию
                }).ToList();

                return studentModels;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Children student)
            {
                // Создание новой страницы с информацией о студенте и передача информации о нем
                InfoChildPage descriptionPage = new InfoChildPage(student);
                NavigationService.Navigate(descriptionPage);
            }
                
        }
    }

}
