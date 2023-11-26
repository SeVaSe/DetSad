﻿using DetSad.Classes;
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

namespace DetSad.AdditPages
{
    /// <summary>
    /// Логика взаимодействия для MenuUserPage.xaml
    /// </summary>
    public partial class MenuUserPage : Page
    {
        public MenuUserPage()
        {
            InitializeComponent();


            string nameUser = InfoUserControl.GetLogin();
            MessageBox.Show(nameUser);

            using (var db = new KindergartenDBEntities())
            {
                var context = db.Users.FirstOrDefault(c => c.Username == nameUser);
                string role = context.Role;

                switch (role)
                {
                    case "admin":
                        TxtBl_Role.Text = "Администрация";
                        break;
                    case "doctor":
                        TxtBl_Role.Text = "Мед-Работник";
                        break;
                    case "teacher":
                        TxtBl_Role.Text = "Воспитатель";
                        break;
                }                   
                    
                TxtBl_NameFio.Text = context.UserFIO;
            }
        }
    }
}
