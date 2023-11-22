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
            Loaded += Dogovor_Loaded;
        }

        private void Dogovor_Loaded(object sender, RoutedEventArgs e)
        {
            List<DogovorModel> childInfoList = new List<DogovorModel>();

            using (var db = new KindergartenDBEntities())
            {
                var children = db.Children.ToList();

                foreach (var child in children)
                {
                    string nameDogovor;
                    if (child.ContractID != null)
                    {
                        var dogovoRecord = db.Contracts.FirstOrDefault(m => m.ContractID == child.ContractID);
                        nameDogovor = dogovoRecord != null ? dogovoRecord.DocumentName : "нет договора";
                    }
                    else
                    {
                        nameDogovor = "нет договора";
                    }

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

                    childInfoList.Add(info);
                }
            }

            EventsDataGrid.ItemsSource = childInfoList;
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Проверка отправителя события и получение данных модели Assignments
            if (sender is Border border && border.DataContext is DogovorModel dogovor)
            {
                // Создание новой страницы с описанием задания и переход на нее
                AdditPages.CreateOpenDogovorPage descriptionPage = new AdditPages.CreateOpenDogovorPage(dogovor);
                NavigationService.Navigate(descriptionPage);
            }
        }
    }
}
