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
    /// Логика взаимодействия для AdmMedPage.xaml
    /// </summary>
    public partial class AdmMedPage : Page
    {
        public AdmMedPage()
        {
            InitializeComponent();
            Loaded += MedSpravka_Loaded; // Подписываемся на событие загрузки страницы
        }

        private void MedSpravka_Loaded(object sender, RoutedEventArgs e)
        {
            // Создаем список для хранения информации о медицинских справках детей
            List<MedSpravkaModel> childInfoList = new List<MedSpravkaModel>();

            using (var db = new KindergartenDBEntities())
            {
                var children = db.Children.ToList(); // Получаем список всех детей

                foreach (var child in children)
                {
                    string nameSpravka;

                    // Проверяем наличие медицинской справки у ребенка
                    if (child.MedicalCertificateID != null)
                    {
                        // Получаем информацию о справке ребенка
                        var medRecord = db.MedicalRecords.FirstOrDefault(m => m.RecordID == child.MedicalCertificateID);
                        nameSpravka = medRecord != null ? medRecord.DocumentName : "нет справки";
                    }
                    else
                    {
                        nameSpravka = "нет справки";
                    }

                    // Создаем модель с информацией о медицинской справке ребенка и добавляем в список
                    MedSpravkaModel info = new MedSpravkaModel
                    {
                        ChildID = child.ChildID,
                        FIO = child.ChildName,
                        Birth = child.DateOfBirth.ToString(),
                        Allergy = child.Allergy,
                        NameMom = child.MotherName,
                        NumbMom = child.MotherNumber,
                        NameSpravka = nameSpravka,
                        NameGroup = child.GroupID,
                        MedicalCertificateID = child.MedicalCertificateID
                    };

                    childInfoList.Add(info); // Добавляем информацию о справке в список
                }
            }

            EventsDataGrid.ItemsSource = childInfoList; // Устанавливаем список в DataGrid для отображения на странице
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Проверяем отправителя события и получаем данные модели MedSpravkaModel
            if (sender is Border border && border.DataContext is MedSpravkaModel medSpr)
            {
                // Создаем новую страницу с открытием информации о медицинской справке и переходим на нее
                AdditPages.CreateOpenSpravkaPage descriptionPage = new AdditPages.CreateOpenSpravkaPage(medSpr);
                NavigationService.Navigate(descriptionPage);
            }
        }

    }
}
