using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetSad.Classes
{
    public class InfoUserControl
    {
        private static string login = "noap"; // Строка по умолчанию для логина

        // Метод для установки значения логина
        public static void SetLogin(string log)
        {
            login = log; // Присваивание переданного значения логина переменной
        }

        // Метод для получения текущего значения логина
        public static string GetLogin()
        {
            return login; // Возвращение текущего значения логина
        }
    }



    public class InfoChildControl
    {
        private static int idChild = 9999; // Строка по умолчанию для логина

        // Метод для установки значения логина
        public static void SetLogin(int log)
        {
            idChild = log; // Присваивание переданного значения логина переменной
        }

        // Метод для получения текущего значения логина
        public static int GetLogin()
        {
            return idChild; // Возвращение текущего значения логина
        }
    }


    public class ChildIDControl
    {
        private static int idChild = 9999; // Строка по умолчанию для логина

        // Метод для установки значения логина
        public static void SetLogin(int log)
        {
            idChild = log; // Присваивание переданного значения логина переменной
        }

        // Метод для получения текущего значения логина
        public static int GetLogin()
        {
            return idChild; // Возвращение текущего значения логина
        }
    }
}
