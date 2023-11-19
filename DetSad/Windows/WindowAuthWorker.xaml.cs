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
using System.Windows.Shapes;

namespace DetSad.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAuthWorker.xaml
    /// </summary>
    public partial class WindowAuthWorker : Window
    {
        public WindowAuthWorker()
        {
            InitializeComponent();
            ControlClass cntrlCl = new ControlClass(this);
            Btn_Close.Click += cntrlCl.close_control;
            Btn_minim.Click += cntrlCl.minimized_control;
            Btn_perezapusk.Click += (sender, e) => cntrlCl.perezapusk_control(new MainWindow());
            br_up.MouseLeftButtonDown += cntrlCl.Window_MouseLeftButtonDown;
            br_up.MouseMove += cntrlCl.Window_MouseMove;
        }

        private void SignUser_Click(object sender, RoutedEventArgs e)
        {
           // var mainWindow = Window.GetWindow(this) as MainWindow;

            using (var db = new KindergartenDBEntities()) // Использование контекста базы данных
            {
                var user = db.Users
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Username == TxtBox_Login.Text); // Поиск пользователя по логину

                if (user == null) // Если пользователь не найден
                {
                    // Отображение сообщения об ошибке
                    MessageBox.Show("Такого пользователя не существует!", "Не существующий пользователь", MessageBoxButton.OK, MessageBoxImage.Error);
                    TxtBox_Login.Clear();
                    TxtBox_Pasword.Clear();
                }
                else if (TxtBox_Pasword.Text.Length >= 6) // Если пароль длиннее или равен 6 символам
                {
                    switch (user.Role) // Проверка роли пользователя
                    {
                        case "admin":
                            if (user.Password != TxtBox_Pasword.Text) // Если пароль не совпадает
                            {
                                // Отображение предупреждения о неверном пароле
                                MessageBox.Show("Пароль указан не верно", "Ошибка пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
                                TxtBox_Pasword.Clear();
                            }
                            else
                            {
                                // Открытие окна для администратора и закрытие текущего окна
                                InfoUserControl.SetLogin(user.Username);
                                OpenWindowClass.OpenWindow<WindowAdmin>();
                                this.Close();
                            }
                            break;
                        case "teacher":
                            if (user.Password != TxtBox_Pasword.Text) // Если хэшированный пароль не совпадает
                            {
                                // Отображение предупреждения о неверном пароле
                                MessageBox.Show("Пароль указан не верно", "Ошибка пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
                                TxtBox_Pasword.Clear();
                            }
                            else
                            {
                                // Открытие окна для пользователя и закрытие текущего окна
                                InfoUserControl.SetLogin(user.Username);
                                OpenWindowClass.OpenWindow<WindowTeacher>();
                                this.Close();
                            }
                            break;
                        case "doctor":
                            if (user.Password != TxtBox_Pasword.Text) // Если хэшированный пароль не совпадает
                            {
                                // Отображение предупреждения о неверном пароле
                                MessageBox.Show("Пароль указан не верно", "Ошибка пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
                                TxtBox_Pasword.Clear();
                            }
                            else
                            {
                                // Открытие окна для пользователя и закрытие текущего окна
                                InfoUserControl.SetLogin(user.Username);
                                OpenWindowClass.OpenWindow<WindowDoctor>();
                                this.Close();
                            }
                            break;
                    }
                    TxtBox_Login.Clear();
                    TxtBox_Pasword.Clear();
                }
                else if (TxtBox_Pasword.Text.Length < 6) // Если пароль короче 6 символов
                {
                    // Отображение сообщения о коротком пароле
                    MessageBox.Show("Вы указали маленький пароль", "Маленький пароль", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TxtBox_Pasword.Clear();
                }
            }
        }
    }
}
