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

namespace DetSad.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdmCalendarPage.xaml
    /// </summary>
    public partial class AdmCalendarPage : Page
    {
        public AdmCalendarPage()
        {
            InitializeComponent();
            Loaded += CalendarPage_Loaded; // Подписываемся на событие загрузки страницы
            TxtBl_Mounth.Text = DateTime.Now.ToString("MMMM yyyy"); // Устанавливаем текущий месяц и год в текстовом поле
        }

        public int GetTeacherIDByLogin(string username)
        {
            // Метод для получения ID учителя по его имени пользователя
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

        public class EventWithGroupInfo
        {
            // Модель, содержащая информацию о событиях и связанной с ними информации о группе
            public string EventDate { get; set; }
            public string EventTime { get; set; }
            public string EventName { get; set; }
            public int GroupID { get; set; } // Информация о группе для каждого события
        }

        public List<EventWithGroupInfo> GetEventsForAllGroupsAndMonth(int month)
        {
            // Получение всех событий с информацией о группах для указанного месяца
            using (var context = new KindergartenDBEntities())
            {
                var allEventsWithGroups = context.EventsSchedule
                    .Select(e => new EventWithGroupInfo
                    {
                        EventDate = e.EventDate,
                        EventTime = e.EventTime.ToString().Substring(0, 5),
                        EventName = e.EventName,
                        GroupID = e.GroupID // Добавляем информацию о группе к событию
                    })
                    .ToList(); // Получаем все события с информацией о группах

                var eventsForMonth = allEventsWithGroups
                    .Where(e => int.Parse(e.EventDate.Split('.')[1]) == month) // Фильтруем по месяцу
                    .ToList();

                return eventsForMonth; // Возвращаем список событий для указанного месяца
            }
        }

        public int GetTeacherGroupID(int teacherID)
        {
            // Метод для получения ID группы учителя по его ID
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
            // Получаем текущий месяц и год
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;

            // Получаем мероприятия для всех групп и текущего месяца
            List<EventWithGroupInfo> eventsForAllGroupsAndMonth = GetEventsForAllGroupsAndMonth(currentMonth);

            // Устанавливаем полученные события в DataGrid для отображения на странице
            EventsDataGrid.ItemsSource = eventsForAllGroupsAndMonth;
        }


    }
}
