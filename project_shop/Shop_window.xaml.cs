﻿using System;
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

namespace project_shop
{
    /// <summary>
    /// Логика взаимодействия для Shop_window.xaml
    /// </summary>
    public partial class Shop_window : Window
    {
        public Shop_window()
        {
            InitializeComponent();
            Frame_Shop.Content = new Main_Shop_Window();
        }
    }
}
