using DetSad.AdditPages;
using DetSad.Classes;
using DetSad.DateBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

            DateTime t = DateTime.Now;
            TxtBl_Date.Text = $"Дата: {t.ToShortDateString()}";
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




        // Метод для получения списка студентов для данной группы
        public List<StudentModel> GetStudentsByGroupID(int groupID)
        {
            using (var context = new KindergartenDBEntities())
            {
                var students = context.Children.Where(s => s.GroupID == groupID).ToList();

                List<StudentModel> studentModels = students.Select(s => new StudentModel
                {
                     ChildID = s.ChildID,
                     FIO = s.ChildName,
                     Birth = s.DateOfBirth.ToString(),
                     MomName = s.MotherName,
                     NumbMom = s.MotherNumber,
                     DadName = s.FatherName,
                     NumbDad = s.FatherNumber,
                     Allergy = s.Allergy,
                     MedID = s.MedicalCertificateID,
                     ContarctID = s.ContractID,
                     Group = context.Groups.Where(w => w.GroupID == s.GroupID).Select(g => g.GroupName).FirstOrDefault()


                }).ToList();

                return studentModels;
            }
        }


        // Событие загрузки страницы
        private void GroupPage_Loaded(object sender, RoutedEventArgs e)
        {
            string username = InfoUserControl.GetLogin(); // Получаем логин учителя

            int teacherID = GetTeacherIDByLogin(username); // Получаем ID учителя
            int groupID = GetTeacherGroupID(teacherID); // Получаем ID группы учителя

            // Получаем список студентов для данной группы учителя
            List<StudentModel> students = GetStudentsByGroupID(groupID);

            using (var c = new KindergartenDBEntities())
            {
                TxtBl_Group.Text = c.Groups.FirstOrDefault(s => s.GroupID == groupID)?.GroupName;
                // Получаем актуальное состояние флажков из базы данных
                foreach (var student in students)
                {
                    var dbStudent = c.Children.FirstOrDefault(s => s.ChildID == student.ChildID);
                    if (dbStudent != null)
                    {
                        student.IsHere = dbStudent.IsHere ?? false;
                    }
                }
            }

            


            // Загружаем студентов в DataGrid
            EventsDataGrid.ItemsSource = students;
        }

        // Обновляет состояние присутствия студента в базе данных
        public async Task UpdateIsHereInDatabase(string studentFIO, bool isHere)
        {
            using (var context = new KindergartenDBEntities())
            {
                var student = context.Children.FirstOrDefault(s => s.ChildName == studentFIO);
                if (student != null)
                {
                    student.IsHere = isHere;
                    await context.SaveChangesAsync();
                }
            }
        }

        // Обработчик события "Checked" у CheckBox
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                StudentModel student = checkBox.DataContext as StudentModel;
                if (student != null)
                {
                    UpdateIsHereInDatabase(student.FIO, true);
                }
            }
        }

        // Обработчик события "Unchecked" у CheckBox
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                StudentModel student = checkBox.DataContext as StudentModel;
                if (student != null)
                {
                    UpdateIsHereInDatabase(student.FIO, false);
                }
            }
        }

        // Обработчик события щелчка по Border для получения дополнительной информации о студенте
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border)
            {
                if (border.DataContext is StudentModel student)
                {
                    InfoChildPage descriptionPage = new InfoChildPage(student);
                    NavigationService?.Navigate(descriptionPage);
                }
                else
                {
                    MessageBox.Show($"Таких данных не существует");
                }
            }
            else
            {
                MessageBox.Show("Таких данных не существует");
            }
        }

       

    }
}
