using DetSad.Pages.AdminPages;
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
using System.IO;
using System.Text.RegularExpressions;
using Word = Microsoft.Office.Interop.Word;
using DetSad.DateBase;
using System.Diagnostics.Contracts;
using DetSad.Classes;
using DetSad.Windows;

namespace DetSad.AdditPages
{
    /// <summary>
    /// Логика взаимодействия для CreateDogovorPage.xaml
    /// </summary>
    public partial class CreateDogovorPage : Page
    {
        public CreateDogovorPage()
        {
            InitializeComponent();
        }

        private void ButtonBackUp_Click(object sender, RoutedEventArgs e)
        {
            InfoChildControl.SetLogin(9999);
            NavigationService?.Navigate(new AdmDogovorPage());
        }

        private void ButtonDownloadSpravka_Click(object sender, RoutedEventArgs e)
        {
            string templatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ISH\\templateContract.docx");
            string destinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ContrFolder");

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            if (TxtBox_NumbMom.Text.Length != 11)
            {
                MessageBox.Show("Вы ввели не правильный пароль, в нем должно быть 11 символов");
                return;
            }
            DateTime timeNow = DateTime.Now;


            string newFilePath = System.IO.Path.Combine(destinationPath, $"Contract_{TxtBox_StartContract.Text}_{TxtBox_EndContract.Text}.docx");

            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Open(templatePath);

            // Замена триггерных слов на значения из TextBox'ов
            ReplaceText(doc, "FIOChild", TxtBox_FIOChild.Text);
            ReplaceText(doc, "Birth", TxtBox_Birth.Text);
            ReplaceText(doc, "FIOmom", TxtBox_Mom.Text);
            ReplaceText(doc, "AdresChild", TxtBox_Adres.Text);
            ReplaceText(doc, "Pasp", TxtBox_SeriaNumPasp.Text);
            ReplaceText(doc, "IssuedBy", TxtBox_IssuedBy.Text);
            ReplaceText(doc, "DateIssuedBy", TxtBox_DateIssuance.Text);
            ReplaceText(doc, "Date", timeNow.ToString());
            ReplaceText(doc, "Group", TxtBox_GroupCh.Text);





            doc.SaveAs2(newFilePath);
            doc.Close();
            wordApp.Quit();

            using (var db = new KindergartenDBEntities())
            {
                // Создание новой записи о договоре
                var newContract = new Contracts
                {
                    DocumentName = $"Contract_{TxtBox_StartContract.Text}_{TxtBox_EndContract.Text}.docx",
                };

                db.Contracts.Add(newContract);
                db.SaveChanges();

                // Получение ID только что добавленного договора
                int newContractID = newContract.ContractID;
                int child_id = InfoChildControl.GetLogin();

                // Нахождение ребенка в базе данных по имени (здесь предполагается, что у вас есть уникальное поле для имени ребенка)
                var child = db.Children.FirstOrDefault(c => c.ChildID == child_id);
                if (child != null)
                {
                    // Связывание ребенка и договора по их идентификаторам
                    child.ContractID = newContractID;
                    db.SaveChanges();
                }
            }



            MessageBox.Show("Договор создан");
            InfoChildControl.SetLogin(9999);
            NavigationService?.Navigate(new AdmDogovorPage());
        }

        private void ReplaceText(Word.Document doc, string placeholder, string text)
        {
            Word.Range range = doc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: placeholder, ReplaceWith: text, Replace: Word.WdReplace.wdReplaceAll);
        }
    }
}
