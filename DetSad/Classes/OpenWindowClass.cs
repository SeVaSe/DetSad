using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DetSad.Classes
{
    public class OpenWindowClass
    {
        public static void OpenWindow<T>() where T : Window, new()
        {
            Window wind = new T();
            wind.Show();
        }
    }
}
