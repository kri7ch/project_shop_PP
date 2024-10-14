using Microsoft.Win32;
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
using Newtonsoft.Json;
using System.IO;

namespace project_shop
{
    /// <summary>
    /// Логика взаимодействия для Aut.xaml
    /// </summary>
    public partial class Aut : Page
    {
        public Aut()
        {
            InitializeComponent();
        }
        public void Entry_window()
        {
            var window = Window.GetWindow(this);
            window.Title = "Регистрация";
            NavigationService.Navigate(new Regist());
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Entry_window();
        }
        private void Checking_the_data()
        {
            string email = textbox_email.Text;
            string password = textbox_password.Password;
            string prominent_password = textbox_show_password.Text;

            if (checkbox_password.IsChecked == true)
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(prominent_password))
                {
                    MessageBox.Show("Заполнены не все поля! Заполните поля и возвращайтесь вновь", "Предупреждение: Пустые поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Заполнены не все поля! Заполните поля и возвращайтесь вновь", "Предупреждение: Пустые поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }

            string json = File.ReadAllText("users.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            User user;

            if (checkbox_password.IsChecked == true)
            {
                user = users.FirstOrDefault(u => u.Email == email && u.Password == prominent_password);
            }
            else
            {
                user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
            }

            if (user != null)
            {
                MessageBox.Show("Вы успешно вошли в аккаунт!", "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);
                broadcast(user.Id);
                Shop_window shop_Window = new Shop_window(user.Id);
                shop_Window.Show();
                Window.GetWindow(this)?.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Предупреждение: неверные данные", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        public void broadcast(int id)
        {
            Profile_Page profilePage = new Profile_Page(id);
            Main_Shop_Window main_Shop_Window = new Main_Shop_Window(id);
            Basket_Page basket_Page = new Basket_Page(id);
        }
        private void checkbox_password_Checked(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(textbox_password, textbox_show_password, checkbox_password.IsChecked == true);
        }

        private void checkbox_password_Unchecked(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(textbox_password, textbox_show_password, false);
        }

        private void TogglePasswordVisibility(PasswordBox passwordBox, TextBox showPasswordBox, bool isChecked)
        {
            if (isChecked)
            {
                showPasswordBox.Text = passwordBox.Password;
                passwordBox.Visibility = Visibility.Hidden;
                RemovePasswordBox(showPasswordBox);
            }
            else
            {
                passwordBox.Password = showPasswordBox.Text;
                showPasswordBox.Visibility = Visibility.Hidden;
                RestorePasswordBox(passwordBox);
            }
        }

        private void RemovePasswordBox(TextBox showPasswordBox)
        {
            showPasswordBox.Visibility = Visibility.Visible;
            showPasswordBox.Focus();
            showPasswordBox.Select(showPasswordBox.Text.Length, 0);
        }

        private void RestorePasswordBox(PasswordBox passwordBox)
        {
            passwordBox.Visibility = Visibility.Visible;
            passwordBox.Focus();
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        private void btn_enter_Click(object sender, RoutedEventArgs e)
        {
            Checking_the_data();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                ShowImageForTextBox(textBox.Name, Visibility.Visible);
            }
            else if (sender is PasswordBox passwordBox)
            {
                ShowImageForTextBox(passwordBox.Name, Visibility.Visible);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                ShowImageForTextBox(textBox.Name, Visibility.Hidden);
            }
            else if (sender is PasswordBox passwordBox)
            {
                ShowImageForTextBox(passwordBox.Name, Visibility.Hidden);
            }
        }

        private void ShowImageForTextBox(string textBoxName, Visibility visibility)
        {
            switch (textBoxName)
            {
                case "textbox_email":
                    Image_Email.Visibility = visibility;
                    break;
                case "textbox_password":
                    Image_Password.Visibility = visibility;
                    break;
                case "textbox_show_password":
                    Image_Password.Visibility = visibility;
                    break;
            }
        }

        public void Guest_Page()
        {
            var window = Window.GetWindow(this);
            window.Title = "Регистрация";
            NavigationService.Navigate(new MainGuestPage());
        }

        private void Hyperlink_Click_Guest(object sender, RoutedEventArgs e)
        {
            Guest_Page();
        }


    }
}
