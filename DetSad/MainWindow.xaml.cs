using DetSad.Classes;
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
using DetSad.Windows;
using System.Windows.Threading;

namespace DetSad
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fullText = "Добро пожаловать!";
        private int currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            // Создание экземпляра ControlClass для обработки действий с окном
            ControlClass cntrlCl = new ControlClass(this);

            // Привязка обработчиков событий к методам из ControlClass
            Btn_Close.Click += cntrlCl.close_control;
            Btn_minim.Click += cntrlCl.minimized_control;
            Btn_perezapusk.Click += (sender, e) => cntrlCl.perezapusk_control(new MainWindow());
            br_up.MouseLeftButtonDown += cntrlCl.Window_MouseLeftButtonDown;
            br_up.MouseMove += cntrlCl.Window_MouseMove;

            Loaded += YourWindow_Loaded;
        }

        private void WindowAuth_Click(object sender, RoutedEventArgs e)
        {
            // Получение экземпляра MainWindow
            var main = GetWindow(this) as MainWindow;

            // Открытие нового окна WindowAuthWorker и закрытие текущего окна MainWindow
            OpenWindowClass.OpenWindow<WindowAuthWorker>();
            main.Close();
        }

        private async void YourWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Асинхронное добавление текста по буквам при загрузке окна
            await AddTextLetterByLetter();
        }

        private async Task AddTextLetterByLetter()
        {
            // Постепенное добавление символов текста в TxtBl_TextPrew
            while (currentIndex < fullText.Length)
            {
                TxtBl_TextPrew.Text += fullText[currentIndex];
                currentIndex++;
                await Task.Delay(70); // Пауза между добавлением символов
            }
        }
    }

}
