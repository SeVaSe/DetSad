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

namespace DetSad.Pages
{
    /// <summary>
    /// Логика взаимодействия для CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            InitializeComponent();
            Loaded += CalendarPage_Loaded; // Подписываемся на событие загрузки страницы
            TxtBl_Mounth.Text = DateTime.Now.ToString("MMMM yyyy");

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


        public List<Event> GetEventsForGroupAndMonth(int groupID, int month)
        {
            using (var context = new KindergartenDBEntities())
            {
                var events = context.EventsSchedule
                    .Where(e => e.GroupID == groupID &&
                                e.EventDate.Substring(3, 5) == month.ToString())
                    .Select(e => new Event
                    {
                        EventDate = e.EventDate,
                        EventTime = e.EventTime.ToString().Substring(0, 5),
                        EventName = e.EventName
                    })
                    .ToList();

                return events;
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


        private void CalendarPage_Loaded(object sender, RoutedEventArgs e)
        {
            string username = InfoUserControl.GetLogin(); // Получаем логин учителя

            int teacherID = GetTeacherIDByLogin(username); // Получаем ID учителя
            int groupID = GetTeacherGroupID(teacherID); // Получаем ID группы учителя

            // Получаем текущий месяц и год
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;
            
            // Получаем мероприятия для текущей группы и текущего месяца
            List<Event> eventsForGroupAndMonth = GetEventsForGroupAndMonth(groupID, currentMonth);

            EventsDataGrid.ItemsSource = eventsForGroupAndMonth;
        }


    }
}
