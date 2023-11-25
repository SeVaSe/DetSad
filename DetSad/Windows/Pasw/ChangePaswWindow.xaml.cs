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

namespace DetSad.Windows.Pasw
{
    /// <summary>
    /// Логика взаимодействия для ChangePaswWindow.xaml
    /// </summary>
    public partial class ChangePaswWindow : Window
    {
        public ChangePaswWindow()
        {
            InitializeComponent();
            ControlClass cntrlCl = new ControlClass(this);
            Btn_Close.Click += cntrlCl.close_control;
            Btn_minim.Click += cntrlCl.minimized_control;
            Btn_perezapusk.Click += (sender, e) => cntrlCl.perezapusk_control(new Pasw.ChangePaswWindow());
            br_up.MouseLeftButtonDown += cntrlCl.Window_MouseLeftButtonDown;
            br_up.MouseMove += cntrlCl.Window_MouseMove;
        }

        private void ChangePasw_Click(object sender, RoutedEventArgs e)
        {
            string newPasw = TxtBox_NewPasw1.Text;
            string confirmPasw = TxtBox_NewPasw2.Text;
            string username = DataDBControlClass.GetName();

            if (TxtBox_NewPasw1.Text != "" || TxtBox_NewPasw2.Text != "")
            {
                if (TxtBox_NewPasw1.Text.Length >= 6)
                {
                    if (newPasw == confirmPasw)
                    {
                        bool passwordChanged = ChangePasswordInDB(username, newPasw);

                        if (passwordChanged)
                        {
                            MessageBox.Show("Пароль успешно изменен!", "Успешное восстановление", MessageBoxButton.OK, MessageBoxImage.Information);
                            DataDBControlClass.SetName("noap");
                            OpenWindowClass.OpenWindow<WindowAuthWorker>();

                        }
                        else
                        {
                            MessageBox.Show("Ошибка при изменении пароля, попробуйте заново!", "Ошибка изменения", MessageBoxButton.OK, MessageBoxImage.Error);
                            TxtBox_NewPasw1.Clear();
                            TxtBox_NewPasw2.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введенные пароли не совпадают", "Не прошла проверка паролей", MessageBoxButton.OK, MessageBoxImage.Error);
                        TxtBox_NewPasw2.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен содержать не менее 6 символов.", "Ошибка ввода пароля", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Вы оставили пустые поля ввода, проверьте и заполните их!", "Ошибка пустого значения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool ChangePasswordInDB(string username, string newPassword)
        {
            using (var db = new KindergartenDBEntities()) // Замените YourDbContext на ваш контекст базы данных
            {
                try
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);

                    if (user != null)
                    {
                        user.Password = newPassword;
                        db.SaveChanges(); // Сохраняем изменения в базе данных

                        return true;
                    }
                    else
                    {
                        // Пользователь не найден
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
