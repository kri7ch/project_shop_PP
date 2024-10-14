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
using Newtonsoft.Json;
using System.IO;

namespace project_shop
{
    /// <summary>
    /// Логика взаимодействия для Shop_window.xaml
    /// </summary>
    public partial class Shop_window : Window
    {
        private int currentUserId;
        public Shop_window(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            Frame_Shop.Content = new Main_Shop_Window(currentUserId);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("При выходе корзина будет очищена! Выйти?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                ClearBasket();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ClearBasket()
        {
            string path = "korzina.json";
            if (File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
            }
        }
    }
}
