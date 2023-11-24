using DetSad.DateBase;
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

namespace DetSad.AdditPages
{
    /// <summary>
    /// Логика взаимодействия для InfoChildPage.xaml
    /// </summary>
    public partial class InfoChildPage : Page
    {
        public InfoChildPage(Children child)
        {
            InitializeComponent();
            TxtBox_FIOChild.Text = child.ChildName;
            TxtBox_Birth.Text = child.DateOfBirth.ToString();
            TxtBox_Mom.Text = child.MotherName;
            TxtBox_NumbMom.Text = child.MotherNumber;
            TxtBox_Dad.Text = child.FatherName;
            TxtBox_NumbDad.Text = child.FatherNumber;
            TxtBox_Allergy.Text = child.Allergy;
            
        }

        private void ButtonDownloadSpravka_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonBackUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MyGroupPage());
        }
    }
}
