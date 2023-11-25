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

namespace DetSad.Windows.Pasw
{
    /// <summary>
    /// Логика взаимодействия для PaswWindow.xaml
    /// </summary>
    public partial class PaswWindow : Window
    {
        public PaswWindow()
        {
            InitializeComponent();
            ControlClass cntrlCl = new ControlClass(this);
            Btn_Close.Click += cntrlCl.close_control;
            Btn_minim.Click += cntrlCl.minimized_control;
            Btn_perezapusk.Click += (sender, e) => cntrlCl.perezapusk_control(new Pasw.PaswWindow());
            br_up.MouseLeftButtonDown += cntrlCl.Window_MouseLeftButtonDown;
            br_up.MouseMove += cntrlCl.Window_MouseMove;
        }

        private void CodeVerify_Click(object sender, RoutedEventArgs e)
        {
            string codePasw = ControlCodePaswClass.CodePasw;

            if (TxtBox_Code1.Text.Length >= 2 || TxtBox_Code2.Text.Length >= 2 || TxtBox_Code3.Text.Length >= 2 || TxtBox_Code4.Text.Length >= 2)
            {
                MessageBox.Show("Не правильный код! Вы ввели в одно поле, 2 или более значений, исправьте и попробуйте заново",
                    "Ошибка заполнения полей", MessageBoxButton.OK, MessageBoxImage.Error);

                TxtBox_Code1.Clear();
                TxtBox_Code2.Clear();
                TxtBox_Code3.Clear();
                TxtBox_Code4.Clear();
            }
            else if (TxtBox_Code1.Text != "" && TxtBox_Code2.Text != "" && TxtBox_Code3.Text != "" && TxtBox_Code4.Text != "")
            {
                string fullCodePage = TxtBox_Code1.Text + TxtBox_Code2.Text + TxtBox_Code3.Text + TxtBox_Code4.Text;

                if (fullCodePage == codePasw)
                {
                    MessageBox.Show("Код подтвержден!", "Успешное подтверждения", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenWindowClass.OpenWindow<Pasw.ChangePaswWindow>();
                    ControlCodePaswClass.CodePasw = "noap";
                    this.Close();

                    
                }
                else
                {
                    MessageBox.Show("Не правильный код!", "Провальное подтверждения", MessageBoxButton.OK, MessageBoxImage.Error);

                    TxtBox_Code1.Clear();
                    TxtBox_Code2.Clear();
                    TxtBox_Code3.Clear();
                    TxtBox_Code4.Clear();
                }
            }
            else
            {
                MessageBox.Show("Вы не указали код! Введите код заново", "Ошибка пустого значения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
