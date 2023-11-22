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

            string newFilePath = System.IO.Path.Combine(destinationPath, $"Contract_{TxtBox_StartContract.Text}_{TxtBox_EndContract.Text}.docx");

            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Open(templatePath);

            // Замена триггерных слов на значения из TextBox'ов
            ReplaceText(doc, "FIOChild", TxtBox_FIOChild.Text);
            ReplaceText(doc, "Birth", TxtBox_Birth.Text);
            ReplaceText(doc, "FIOmom", TxtBox_Mom.Text);
            // Продолжите для остальных TextBox'ов...

            doc.SaveAs2(newFilePath);
            doc.Close();
            wordApp.Quit();

            MessageBox.Show("Договор создан");
        }

        private void ReplaceText(Word.Document doc, string placeholder, string text)
        {
            Word.Range range = doc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: placeholder, ReplaceWith: text, Replace: Word.WdReplace.wdReplaceAll);
        }
    }
}
