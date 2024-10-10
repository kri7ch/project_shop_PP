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
                Shop_window shop_Window = new Shop_window();
                shop_Window.Show();
                Window.GetWindow(this)?.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Предупреждение: неверные данные", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        public void Remove_password_box()
        {
            textbox_show_password.Visibility = Visibility.Visible;
            textbox_password.Visibility = Visibility.Hidden;
        }
        public void Restore_password_box()
        {
            textbox_password.Visibility = Visibility.Visible;
            textbox_show_password.Visibility = Visibility.Hidden;
        }

        public void Checked_Pass()
        {
            if (checkbox_password.IsChecked == true)
            {
                textbox_show_password.Text = textbox_password.Password;
                Remove_password_box();
                textbox_show_password.Focus();
                textbox_show_password.Select(textbox_show_password.Text.Length, 0);
                if (checkbox_password.IsChecked == false)
                {
                    textbox_password.Password = textbox_show_password.Text;
                    Restore_password_box();
                    textbox_password.Focus();
                }
            }
        }
        public void Unchecked_Pass()
        {
            if (checkbox_password.IsChecked == false)
            {
                textbox_password.Password = textbox_show_password.Text;
                Restore_password_box();
                textbox_password.Focus();
                if (checkbox_password.IsChecked == true)
                {
                    textbox_show_password.Text = textbox_password.Password;
                    Remove_password_box();
                    textbox_show_password.Focus();
                    textbox_show_password.Select(textbox_show_password.Text.Length, 0);
                }
            }
        }
        private void checkbox_password_Checked(object sender, RoutedEventArgs e)
        {
            Checked_Pass();
        }

        private void checkbox_password_Unchecked(object sender, RoutedEventArgs e)
        {
            Unchecked_Pass();
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
        private void textbox_password_GotFocus(object sender, RoutedEventArgs e)
        {
            Image_Email.Visibility = Visibility.Visible;
        }

        private void textbox_password_LostFocus(object sender, RoutedEventArgs e)
        {
            Image_Email.Visibility = Visibility.Hidden;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Image_Password.Visibility = Visibility.Visible;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Image_Password.Visibility = Visibility.Hidden;
        }
        private void textbox_show_password_GotFocus(object sender, RoutedEventArgs e)
        {
            Image_Password.Visibility = Visibility.Visible;
        }

        private void textbox_show_password_LostFocus(object sender, RoutedEventArgs e)
        {
            Image_Password.Visibility = Visibility.Hidden;
        }
    }
}
