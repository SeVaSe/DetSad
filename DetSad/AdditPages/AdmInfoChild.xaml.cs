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
        // Конструктор страницы, который принимает модель StudentModel
        public AdmInfoChild(StudentModel chd)
        {
            InitializeComponent();

            // Устанавливаем текстовые поля страницы в соответствии с данными из модели StudentModel
            TxtBox_FIOChild.Text = chd.FIO;
            TxtBox_Birth.Text = chd.Birth;
            TxtBox_Mom.Text = chd.MomName;
            TxtBox_NumbMom.Text = chd.NumbMom;
            TxtBox_Dad.Text = chd.DadName;
            TxtBox_NumbDad.Text = chd.NumbDad;
            TxtBox_Allergy.Text = chd.Allergy;
            TxtBl_NameGroup.Text = chd.Group.Substring(0, 6) + ".";

            // Подключаемся к базе данных
            using (var sp = new KindergartenDBEntities())
            {
                // Получаем информацию о медицинской справке и договоре из БД
                var medS = sp.MedicalRecords.FirstOrDefault(s => s.RecordID == chd.MedID);
                var conS = sp.Contracts.FirstOrDefault(s => s.ContractID == chd.ContarctID);

                // Устанавливаем текстовые поля для отображения информации о медицинской справке и договоре
                TxtBox_Spravka.Text = medS != null ? medS.DocumentName : "Нет информации о справке";
                TxtBox_Dogovor.Text = conS != null ? conS.DocumentName : "Нет информации о договоре";

                // Получаем информацию о студенте по его ID
                var student = sp.Children.FirstOrDefault(s => s.ChildID == chd.ChildID);
                if (student != null)
                {
                    try
                    {
                        string photoFileName = student.ImagePath; // Получаем имя файла фотографии

                        // Формируем путь к файлу фотографии
                        string photoFolderPath = System.IO.Path.Combine(Environment.CurrentDirectory, "PhotoCh");
                        string fullPath = System.IO.Path.Combine(photoFolderPath, photoFileName);

                        // Загружаем изображение в элемент Image (ImgChild)
                        if (System.IO.File.Exists(fullPath))
                        {
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(fullPath);
                            bitmap.EndInit();
                            ImgChild.Source = bitmap;
                        }
                    }
                    catch
                    {
                        // Если возникает ошибка при загрузке фотографии, устанавливаем фото по умолчанию
                        string photoFolderPath1 = System.IO.Path.Combine(Environment.CurrentDirectory, "PhotoCh");
                        string fullPath1 = System.IO.Path.Combine(photoFolderPath1, "DEFAULT.png");

                        BitmapImage defaultBitmap = new BitmapImage();
                        defaultBitmap.BeginInit();
                        defaultBitmap.UriSource = new Uri(fullPath1);
                        defaultBitmap.EndInit();
                        ImgChild.Source = defaultBitmap;
                    }
                }
                else
                {
                    MessageBox.Show("Сотрудник не найден в БД");
                }
            }
        }

        // Обработчик нажатия кнопки "Назад", переход на страницу AdmGroupPage
        private void ButtonBackUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AdmGroupPage());
        }

        // Обработчик нажатия кнопки "Изменить", сохранение изменений в базе данных
        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new KindergartenDBEntities())
            {
                // Находим ребенка в базе данных по его имени
                var childToUpdate = db.Children.FirstOrDefault(c => c.ChildName == TxtBox_FIOChild.Text);

                if (childToUpdate != null)
                {
                    // Обновляем данные ребенка в базе данных с учетом изменений
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

                // Сохраняем изменения в базе данных и выводим сообщение об успешном изменении
                db.SaveChanges();
                MessageBox.Show("Вы успешно изменили данные");
            }
        }
    }

}
