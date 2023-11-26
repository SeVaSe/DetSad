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
            // Создаем список моделей для информации о медицинских справках детей
            List<MedSpravkaModel> childInfoList = new List<MedSpravkaModel>();

            // Подключение к базе данных
            using (var db = new KindergartenDBEntities())
            {
                // Получаем список всех детей из базы данных
                var children = db.Children.ToList();

                // Перебираем каждого ребенка и формируем информацию о его медицинской справке
                foreach (var child in children)
                {
                    string nameSpravka;
                    // Проверяем наличие медицинской справки у ребенка
                    if (child.MedicalCertificateID != null)
                    {
                        // Получаем информацию о медицинской справке из базы данных
                        var medRecord = db.MedicalRecords.FirstOrDefault(m => m.RecordID == child.MedicalCertificateID);
                        nameSpravka = medRecord != null ? medRecord.DocumentName : "нет справки";
                    }
                    else
                    {
                        nameSpravka = "нет справки";
                    }

                    // Формируем модель для информации о ребенке и его медицинской справке
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

                    // Добавляем информацию о ребенке в список
                    childInfoList.Add(info);
                }
            }

            // Устанавливаем список данных в DataGrid для отображения на странице
            EventsDataGrid.ItemsSource = childInfoList;
        }

        // Обработчик события клика по Border для перехода к информации о медицинской справке
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Проверяем, что источник события - Border, и его контекст данных - MedSpravkaModel
            if (sender is Border border && border.DataContext is MedSpravkaModel medSpr)
            {
                // Создаем новую страницу для открытия информации о медицинской справке и переходим на нее
                AdditPages.CreateOpenSpravkaPage descriptionPage = new AdditPages.CreateOpenSpravkaPage(medSpr);
                NavigationService.Navigate(descriptionPage);
            }
        }


    }
}
