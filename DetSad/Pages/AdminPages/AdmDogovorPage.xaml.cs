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
    /// Логика взаимодействия для AdmDogovorPage.xaml
    /// </summary>
    public partial class AdmDogovorPage : Page
    {
        public AdmDogovorPage()
        {
            InitializeComponent();
            Loaded += Dogovor_Loaded; // Подписываемся на событие загрузки страницы
        }

        private void Dogovor_Loaded(object sender, RoutedEventArgs e)
        {
            // Создаем список для хранения информации о договорах ребенка
            List<DogovorModel> childInfoList = new List<DogovorModel>();

            using (var db = new KindergartenDBEntities())
            {
                var children = db.Children.ToList(); // Получаем список всех детей

                foreach (var child in children)
                {
                    string nameDogovor;

                    // Проверяем наличие договора у ребенка
                    if (child.ContractID != null)
                    {
                        // Получаем информацию о договоре ребенка
                        var dogovoRecord = db.Contracts.FirstOrDefault(m => m.ContractID == child.ContractID);
                        nameDogovor = dogovoRecord != null ? dogovoRecord.DocumentName : "нет договора";
                    }
                    else
                    {
                        nameDogovor = "нет договора";
                    }

                    // Создаем модель с информацией о договоре ребенка и добавляем в список
                    DogovorModel info = new DogovorModel
                    {
                        ChildID = child.ChildID,
                        FIO = child.ChildName,
                        Birth = child.DateOfBirth.ToString(),
                        Allergy = child.Allergy,
                        NameMom = child.MotherName,
                        NumbMom = child.MotherNumber,
                        NameDogovor = nameDogovor,
                        NameGroup = child.GroupID,
                        DogovorCertificateID = child.ContractID
                    };

                    childInfoList.Add(info); // Добавляем информацию о договоре в список
                }
            }

            EventsDataGrid.ItemsSource = childInfoList; // Устанавливаем список в DataGrid для отображения на странице
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Проверяем отправителя события и получаем данные модели DogovorModel
            if (sender is Border border && border.DataContext is DogovorModel dogovor)
            {
                // Создаем новую страницу с открытием информации о договоре и переходим на нее
                AdditPages.CreateOpenDogovorPage descriptionPage = new AdditPages.CreateOpenDogovorPage(dogovor);
                NavigationService.Navigate(descriptionPage);
            }
        }

    }
}
