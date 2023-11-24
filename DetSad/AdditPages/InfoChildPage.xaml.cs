using DetSad.Classes;
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

        public InfoChildPage(int childID)
        {
            InitializeComponent();

            using (var db = new KindergartenDBEntities())
            {
                var children = db.Children.ToList();

                foreach (var chd in children)
                {
                    if (chd.ChildID == childID)
                    {
                        TxtBox_FIOChild.Text = chd.ChildName;
                        TxtBox_Birth.Text = chd.DateOfBirth.ToString();
                        TxtBox_Mom.Text = chd.MotherName;
                        TxtBox_NumbMom.Text = chd.MotherNumber;
                        TxtBox_Dad.Text = chd.FatherName;
                        TxtBox_NumbDad.Text = chd.FatherNumber;
                        TxtBox_Allergy.Text = chd.Allergy;
                    }
                }


                    
            }


            
            
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
