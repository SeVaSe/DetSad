using DetSad.Classes;
using DetSad.DateBase;
using DetSad.Pages.AdminPages;
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
    /// Логика взаимодействия для AdmInfoChild.xaml
    /// </summary>
    public partial class AdmInfoChild : Page
    {
        public AdmInfoChild(StudentModel chd)
        {
            InitializeComponent();
            MessageBox.Show(chd.ChildID.ToString());

            TxtBox_FIOChild.Text = chd.FIO;
            TxtBox_Birth.Text = chd.Birth;
            TxtBox_Mom.Text = chd.MomName;
            TxtBox_NumbMom.Text = chd.NumbMom;
            TxtBox_Dad.Text = chd.DadName;
            TxtBox_NumbDad.Text = chd.NumbDad;
            TxtBox_Allergy.Text = chd.Allergy;
            TxtBl_NameGroup.Text = chd.Group.Substring(0, 6) + ".";


            using (var sp = new KindergartenDBEntities())
            {
                var medS = sp.MedicalRecords.FirstOrDefault(s => s.RecordID == chd.MedID);
                var conS = sp.Contracts.FirstOrDefault(s => s.ContractID == chd.ContarctID);

                TxtBox_Spravka.Text = medS != null ? medS.DocumentName : "Нет информации о справке";
                TxtBox_Dogovor.Text = conS != null ? conS.DocumentName : "Нет информации о договоре";

                // Получаем информацию о студенте по его ID
                var student = sp.Children.FirstOrDefault(s => s.ChildID == chd.ChildID);
                if (student != null)
                {
                    try
                    {
                        string photoFileName = student.ImagePath; // Предполагается, что в базе хранится имя файла фотографии

                        // Создаем путь к папке "PhotoCh" и добавляем имя файла
                        string photoFolderPath = System.IO.Path.Combine(Environment.CurrentDirectory, "PhotoCh");
                        string fullPath = System.IO.Path.Combine(photoFolderPath, photoFileName);

                        // Теперь у вас есть полный путь к файлу фотографии
                        // Можете использовать этот путь для загрузки изображения
                        if (System.IO.File.Exists(fullPath))
                        {
                            // Пример загрузки изображения в элемент Image (ImgChild)
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(fullPath);
                            bitmap.EndInit();
                            ImgChild.Source = bitmap;
                        }

                    }
                    catch
                    {
                        string photoFolderPath1 = System.IO.Path.Combine(Environment.CurrentDirectory, "PhotoCh");
                        string fullPath1 = System.IO.Path.Combine(photoFolderPath1, "DEFAULT.png");

                        BitmapImage defaultBitmap = new BitmapImage();
                        defaultBitmap.BeginInit();
                        defaultBitmap.UriSource = new Uri(fullPath1);
                        defaultBitmap.EndInit();
                        ImgChild.Source = defaultBitmap;
                    }                    // Получаем имя файла фотографии из базы данных




                }
                else
                {
                    MessageBox.Show("Сотрудник не найден в БД");
                }

            }

        }

        private void ButtonBackUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AdmGroupPage());
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new KindergartenDBEntities())
            {
                var childToUpdate = db.Children.FirstOrDefault(c => c.ChildName == TxtBox_FIOChild.Text);

                if (childToUpdate != null)
                {
                    childToUpdate.ChildName = TxtBox_FIOChild.Text;
                    if (DateTime.TryParse(TxtBox_Birth.Text, out DateTime birthDate))
                    {
                        childToUpdate.DateOfBirth = birthDate;
                    }
                    else
                    {
                        MessageBox.Show("Вы не правильно ввели дату. ГОД-МЕСЯЦ-ДЕНЬ");
                    }
                    childToUpdate.MotherName = TxtBox_Mom.Text;
                    childToUpdate.MotherNumber = TxtBox_NumbMom.Text;
                    childToUpdate.FatherName = TxtBox_Dad.Text;
                    childToUpdate.FatherNumber = TxtBox_NumbDad.Text;  
                    childToUpdate.Allergy = TxtBox_Allergy.Text;
                }

                db.SaveChanges();
                MessageBox.Show("Вы успешно изменили данные");

            }
        }
    }
}
