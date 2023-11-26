using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace DetSad.Classes
{
    public class ControlClass
    {
        private Window _wind;

        public ControlClass(Window wind)
        {
            _wind = wind;
        }

        //Закрыть
        public void close_control(object sender, RoutedEventArgs e)
        {
            var mesClose = MessageBox.Show("Вы точно хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);

            switch (mesClose)
            {
                case MessageBoxResult.Yes:
                    _wind.Close();
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        //Свернуть
        public void minimized_control(object sender, RoutedEventArgs e)
        {
            _wind.WindowState = WindowState.Minimized;
        }

        //Обновить
        public void perezapusk_control(Window newWind)
        {
            newWind.Show();
            _wind.Close();
        }

        //передвижение окна
        public void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                _wind.DragMove();
        }

        public void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                _wind.DragMove();
        }
    }
}
