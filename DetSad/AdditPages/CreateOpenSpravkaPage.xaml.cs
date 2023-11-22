using DetSad.Classes;
using DetSad.DateBase;
using DetSad.Pages.DoctorPages;
using Microsoft.Win32;
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
        private MedSpravkaModel _medSpravka;

        public CreateOpenSpravkaPage(MedSpravkaModel medSpravka)
        {
            InitializeComponent();
            _medSpravka = medSpravka;
            TxtBl_FIO.Text = medSpravka.FIO;
            TxtBl_Birth.Text = medSpravka.Birth;
            TxtBl_Allergy.Text = medSpravka.Allergy;
            TxtBl_NameMom.Text = medSpravka.NameMom;
            TxtBl_NumMom.Text = medSpravka.NumbMom;
            TxtBl_MedSpr.Text = medSpravka.NameSpravka != null ? medSpravka.NameSpravka : "нет справки";
            
            TxtBl_NameGroup.Text = $"Группа \"{GetNameGroup(medSpravka.NameGroup)}\"";

            MessageBox.Show(medSpravka.ChildID.ToString());


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

        private void ButtonOpenSpravka_Click(object sender, RoutedEventArgs e)
        {
            if (_medSpravka.MedicalCertificateID != null)
            {
                using (var db = new KindergartenDBEntities())
                {
                    var medRecord = db.MedicalRecords.FirstOrDefault(m => m.RecordID == _medSpravka.MedicalCertificateID);
                    if (medRecord != null)
                    {
                        string fileName = medRecord.DocumentName; // Имя файла из базы данных
                        string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MedFolder"); // Путь к папке MedFolder внутри папки bin

                        string filePath = System.IO.Path.Combine(folderPath, fileName); // Полный путь к файлу
                        if (System.IO.File.Exists(filePath))
                        {
                            // Если файл существует, откройте его или выполните нужные действия
                            // Например, откройте файл в стандартном приложении для просмотра
                            System.Diagnostics.Process.Start(filePath);
                        }
                        else
                        {
                            MessageBox.Show("Файл не найден");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Справка не найдена в базе данных");
                    }
                }
            }
            else
            {
                MessageBox.Show("У данного пользователя нет медицинской справки");
            }
        }


        private void ButtonDownloadSpravka_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                string destinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MedFolder");

                string newFilePath = System.IO.Path.Combine(destinationPath, System.IO.Path.GetFileName(selectedFileName));

                // Копирование выбранного файла в папку MedFolder
                System.IO.File.Copy(selectedFileName, newFilePath, true);

                using (var db = new KindergartenDBEntities())
                {
                    // Добавление информации о справке в базу данных
                    var newMedRecord = new MedicalRecords { DocumentName = System.IO.Path.GetFileName(selectedFileName) };
                    db.MedicalRecords.Add(newMedRecord);
                    db.SaveChanges();

                    // Привязка справки к ребенку
                    var child = db.Children.FirstOrDefault(c => c.ChildID == _medSpravka.ChildID); // Предполагается, что _medSpravka содержит информацию о ребенке
                    if (child != null)
                    {
                        child.MedicalCertificateID = newMedRecord.RecordID;
                        db.SaveChanges();
                        MessageBox.Show("Справка загружена и привязана к ребенку");
                    }
                    else
                    {
                        MessageBox.Show("Ребенок не найден");
                    }
                }
            }
        }



        private void ButtonBackUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MedSpravkiPage());
        }
    }
}
