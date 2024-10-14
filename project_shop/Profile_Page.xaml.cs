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
using System.IO;
using Newtonsoft.Json;

namespace project_shop
{
    /// <summary>
    /// Логика взаимодействия для Profile_Page.xaml
    /// </summary>
    public partial class Profile_Page : Page
    {
        private int currentUserId;
        public Profile_Page(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            ProvidingInformation();
        }
        private void ProvidingInformation()
        {
            string json = File.ReadAllText("users.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

            User currentUser = users.FirstOrDefault(user => user.Id == currentUserId);

            text_email.Content = currentUser.Email;
            text_surname.Content = currentUser.Surname;
            text_name.Content = currentUser.Name;
            text_lastname.Content = currentUser.Middle_Name;
        }

        public void MainMenu_Page()
        {
            var window = Window.GetWindow(this);
            window.Title = "Главное меню";
            NavigationService.Navigate(new Main_Shop_Window(currentUserId));
        }
        private void Hyperlink_Click_MainMenu(object sender, RoutedEventArgs e)
        {
            MainMenu_Page();
        }

        public void Basket_Page()
        {
            var window = Window.GetWindow(this);
            window.Title = "Корзина";
            NavigationService.Navigate(new Basket_Page(currentUserId));
        }

        private void Hyperlink_Click_Basket(object sender, RoutedEventArgs e)
        {
            Basket_Page();
        }

        public void Exit_account()
        {
            var result = MessageBox.Show("При выходе корзина будет очищена! Выйти?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                ClearBasket();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this)?.Close();
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

        private void Hyperlink_Click_Aut(object sender, RoutedEventArgs e)
        {
            Exit_account();
        }

    }
}
