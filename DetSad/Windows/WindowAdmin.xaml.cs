using DetSad.Classes;
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
    /// Логика взаимодействия для WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        public WindowAdmin()
        {
            InitializeComponent();
            ControlClass cntrlCl = new ControlClass(this);
            Btn_Close.Click += cntrlCl.close_control;
            Btn_minim.Click += cntrlCl.minimized_control;
            Btn_perezapusk.Click += (sender, e) => cntrlCl.perezapusk_control(new WindowAdmin());
            br_up.MouseLeftButtonDown += cntrlCl.Window_MouseLeftButtonDown;
            br_up.MouseMove += cntrlCl.Window_MouseMove;

        }

        private void Dogovor_Click(object sender, RoutedEventArgs e)
        {
            DateFrame.NavigationService?.Navigate(new Pages.AdminPages.AdmDogovorPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateFrame.NavigationService?.Navigate(new Pages.AdminPages.AdmMedPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DateFrame.NavigationService?.Navigate(new Pages.AdminPages.AdmCalendarPage());
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            var winTeac = GetWindow(this) as WindowAdmin;
            OpenWindowClass.OpenWindow<MainWindow>();
            InfoUserControl.SetLogin("noap");
            winTeac.Close();
        }

        
    }
}
