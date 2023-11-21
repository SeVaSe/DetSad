using DetSad.Classes;
using DetSad.DateBase;
using DetSad.Pages.DoctorPages;
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
    /// Логика взаимодействия для CreateOpenSpravkaPage.xaml
    /// </summary>
    public partial class CreateOpenSpravkaPage : Page
    {
        public CreateOpenSpravkaPage(MedSpravkaModel medSpravka)
        {
            InitializeComponent();
            TxtBl_FIO.Text = medSpravka.FIO;
            TxtBl_Birth.Text = medSpravka.Birth;
            TxtBl_Allergy.Text = medSpravka.Allergy;
            TxtBl_NameMom.Text = medSpravka.NameMom;
            TxtBl_NumMom.Text = medSpravka.NumbMom;
            TxtBl_MedSpr.Text = medSpravka.NameSpravka != null ? medSpravka.NameSpravka : "нет справки";

            TxtBl_NameGroup.Text = $"Группа \"{GetNameGroup(medSpravka.NameGroup)}\"";



        }

        public string GetNameGroup(int grID)
        {
            string nameGr = "-";
            using (var db = new KindergartenDBEntities())
            {
                var group = db.Groups.FirstOrDefault(g => g.GroupID == grID);
                if (group != null)
                {
                    nameGr = group.GroupName; // Предположим, что GroupName - это имя поля с названием группы в вашей таблице Groups
                }
            }
            return nameGr;
        }

        private void ButtonBackUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MedSpravkiPage());
        }
    }
}
