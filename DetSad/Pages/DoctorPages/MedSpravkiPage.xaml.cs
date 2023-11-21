using DetSad.DateBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using DetSad.Classes;

namespace DetSad.Pages.DoctorPages
{
    /// <summary>
    /// Логика взаимодействия для MedSpravkiPage.xaml
    /// </summary>
    public partial class MedSpravkiPage : Page
    {
        public MedSpravkiPage()
        {
            InitializeComponent();
            Loaded += MedSpravka_Loaded;
        }

        private void MedSpravka_Loaded(object sender, RoutedEventArgs e)
        {
            List<MedSpravkaModel> childInfoList = new List<MedSpravkaModel>();

            using (var db = new KindergartenDBEntities())
            {
                var children = db.Children.ToList();

                foreach (var child in children)
                {
                    string nameSpravka;
                    if (child.MedicalCertificateID != null)
                    {
                        var medRecord = db.MedicalRecords.FirstOrDefault(m => m.RecordID == child.MedicalCertificateID);
                        nameSpravka = medRecord != null ? medRecord.DocumentName : "нет справки";
                    }
                    else
                    {
                        nameSpravka = "нет справки";
                    }

                    MedSpravkaModel info = new MedSpravkaModel
                    {
                        FIO = child.ChildName,
                        Birth = child.DateOfBirth.ToString(),
                        Allergy = child.Allergy,
                        NameMom = child.MotherName,
                        NumbMom = child.MotherNumber,
                        NameSpravka = nameSpravka,
                        NameGroup = child.GroupID,
                        MedicalCertificateID = child.MedicalCertificateID
                        
                    };

                    childInfoList.Add(info);
                }
            }

            EventsDataGrid.ItemsSource = childInfoList;
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Проверка отправителя события и получение данных модели Assignments
            if (sender is Border border && border.DataContext is MedSpravkaModel medSpr)
            {
                // Создание новой страницы с описанием задания и переход на нее
                AdditPages.CreateOpenSpravkaPage descriptionPage = new AdditPages.CreateOpenSpravkaPage(medSpr);
                NavigationService.Navigate(descriptionPage);
            }
        }

    }
}
