using DetSad.Classes;
using DetSad.Pages;
using DetSad.Pages.TeacherPages;
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

namespace DetSad.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowTeacher.xaml
    /// </summary>
    public partial class WindowTeacher : Window
    {
        public WindowTeacher()
        {
            InitializeComponent();
            ControlClass cntrlCl = new ControlClass(this);
            Btn_Close.Click += cntrlCl.close_control;
            Btn_minim.Click += cntrlCl.minimized_control;
            Btn_perezapusk.Click += (sender, e) => cntrlCl.perezapusk_control(new WindowTeacher());
            br_up.MouseLeftButtonDown += cntrlCl.Window_MouseLeftButtonDown;
            br_up.MouseMove += cntrlCl.Window_MouseMove;


        }

        private void Calendar_Click(object sender, RoutedEventArgs e)
        {
            DetTeachFrame.NavigationService?.Navigate(new CalendarPage());
        }

        private void MeGroup_Click(object sender, RoutedEventArgs e)
        {
            DetTeachFrame.NavigationService?.Navigate(new MyGroupPage());
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            var winTeac = GetWindow(this) as WindowTeacher;
            OpenWindowClass.OpenWindow<MainWindow>();
            InfoUserControl.SetLogin("noap");
            winTeac.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Проверка отправителя события и получение данных модели Assignments
            DetTeachFrame.NavigationService.Navigate(new AdditPages.MenuUserPage());
        }
    }
}
