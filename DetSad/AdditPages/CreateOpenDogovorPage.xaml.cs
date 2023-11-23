using DetSad.Classes;
using DetSad.DateBase;
using DetSad.Pages.AdminPages;
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
    /// Логика взаимодействия для CreateOpenDogovorPage.xaml
    /// </summary>
    public partial class CreateOpenDogovorPage : Page
    {
        private DogovorModel _dogovorDoc;

        public CreateOpenDogovorPage(DogovorModel dogovorDoc)
        {
            InitializeComponent();
            _dogovorDoc = dogovorDoc;
            TxtBl_FIO.Text = dogovorDoc.FIO;
            TxtBl_Birth.Text = dogovorDoc.Birth;
            TxtBl_Allergy.Text = dogovorDoc.Allergy;
            TxtBl_NameMom.Text = dogovorDoc.NameMom;
            TxtBl_NumMom.Text = dogovorDoc.NumbMom;
            TxtBl_MedSpr.Text = dogovorDoc.NameDogovor != null ? dogovorDoc.NameDogovor : "нет договора";

            TxtBl_NameGroup.Text = $"Группа \"{GetNameGroup(dogovorDoc.NameGroup)}\"";

            MessageBox.Show(dogovorDoc.ChildID.ToString());
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
            NavigationService?.Navigate(new AdmDogovorPage());
        }

        private void ButtonOpenSpravka_Click(object sender, RoutedEventArgs e)
        {
            if (_dogovorDoc.DogovorCertificateID != null)
            {
                using (var db = new KindergartenDBEntities())
                {
                    var contrRecord = db.Contracts.FirstOrDefault(m => m.ContractID == _dogovorDoc.DogovorCertificateID);
                    if (contrRecord != null)
                    {
                        string fileName = contrRecord.DocumentName; // Имя файла из базы данных
                        string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ContrFolder"); // Путь к папке MedFolder внутри папки bin

                        string filePath = System.IO.Path.Combine(folderPath, fileName); // Полный путь к файлу
                        if (System.IO.File.Exists(filePath))
                        {
                            System.Diagnostics.Process.Start(filePath);
                        }
                        else
                        {
                            MessageBox.Show("Файл не найден");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Договор не найден в базе данных");
                    }
                }
            }
            else
            {
                MessageBox.Show("У данного пользователя нет договора");
            }
        }

        private void ButtonDownloadSpravka_Click(object sender, RoutedEventArgs e)
        {
            InfoChildControl.SetLogin(_dogovorDoc.ChildID);
            NavigationService?.Navigate(new CreateDogovorPage());
        }
    }
}
