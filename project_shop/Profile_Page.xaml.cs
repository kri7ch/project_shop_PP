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
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace project_shop
{
    /// <summary>
    /// Логика взаимодействия для Profile_Page.xaml
    /// </summary>
    public partial class Profile_Page : Page
    {
        private int currentUserId;
        string json = File.ReadAllText("users.json");
        public Profile_Page(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            ProvidingInformation();
        }
        private void ProvidingInformation()
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

            User currentUser = users.FirstOrDefault(user => user.Id == currentUserId);

            text_email.Content = currentUser.Email;
            text_surname.Content = currentUser.Surname;
            text_name.Content = currentUser.Name;
            text_lastname.Content = currentUser.Middle_Name;
            ordersListView.ItemsSource = currentUser.Orders;
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

        private void textbox_Correct_input_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            TextBox textBox = sender as TextBox;
            if (!Regex.IsMatch(e.Text, @"\p{IsCyrillic}"))
            {
                e.Handled = true;
                return;
            }

            if (textBox.Text.Length == 0)
            {
                e.Handled = true;
                textBox.Text += e.Text.ToUpper();
                textBox.CaretIndex = textBox.Text.Length;
            }
            else
            {
                e.Handled = true;
                textBox.Text += e.Text.ToLower();
                textBox.CaretIndex = textBox.Text.Length;
            }
        }
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste || e.Command == ApplicationCommands.Cut)
            {
                e.Handled = true;
            }
        }
        private void textbox_email_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"\p{IsCyrillic}"))
            {
                e.Handled = true;
            }
        }
        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        private bool Checking_mail()
        {
            bool isEmpty = false;
            
            if (!Regex.IsMatch(textbox_email.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$") || textbox_email.Text.Count(c => c == '@') != 1 ||
                textbox_email.Text.Count(c => c == '.') != 1 || textbox_email.Text.Contains(".."))
            {
                isEmpty = true;
            }
            if (isEmpty)
            {
                MessageBox.Show("Пожалуйста, введите правильный адрес электронной почты.", "Предупреждение: Неверный адрес почты", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            return isEmpty;
        }

        private bool Checking_for_completion()
        {
            bool isEmpty = false;
            List<TextBox> textBoxes = new List<TextBox>
            {
                textbox_email,
                textbox_surname,
                textbox_name
            };
            foreach (TextBox textBox in textBoxes)
            {
                if (textBox.Text == "")
                {
                    isEmpty = true;
                }
            }
            if (isEmpty)
            {
                MessageBox.Show("Заполнены не все поля! Заполните поля и возвращайтесь вновь", "Предупреждение: Пустые поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return isEmpty;
        }

        private void EditingData()
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            User currentUser = users.FirstOrDefault(userInfo => userInfo.Id == currentUserId);
            

            if (Checking_for_completion() || Checking_mail())
            {
                return;
            }

            currentUser.Surname = textbox_surname.Text;
            currentUser.Name = textbox_name.Text;
            currentUser.Middle_Name = textbox_lastname.Text;
            text_surname.Content = currentUser.Surname;

            string newJson = JsonConvert.SerializeObject(users);
            File.WriteAllText("users.json", newJson);
            UpdatingElementsAfterEditing();
            MessageBox.Show("Данные были обновлены!", "Обновление данных", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            EditingData();
        }

        private void ActivatingEditing()
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            User currentUser = users.FirstOrDefault(userInfo => userInfo.Id == currentUserId);

            textbox_email.Text = currentUser.Email; 
            textbox_surname.Text = currentUser.Surname;
            textbox_name.Text = currentUser.Name;
            textbox_lastname.Text = currentUser.Middle_Name;
            UpdatingElementsEditing();
        }
        private void UpdatingElementsEditing()
        {
            text_email.Visibility = Visibility.Hidden;
            text_surname.Visibility = Visibility.Hidden;
            text_name.Visibility = Visibility.Hidden;
            text_lastname.Visibility = Visibility.Hidden;

            textbox_email.Visibility = Visibility.Visible;
            textbox_name.Visibility = Visibility.Visible;
            textbox_lastname.Visibility = Visibility.Visible;
            textbox_surname.Visibility = Visibility.Visible;

            btn_save.Visibility = Visibility.Visible;
            btn_cancel_redact.Visibility = Visibility.Visible;
            btn_redact.Visibility = Visibility.Hidden;
        }

        private void UpdatingElementsAfterEditing()
        {
            text_email.Visibility = Visibility.Visible;
            text_surname.Visibility = Visibility.Visible;
            text_name.Visibility = Visibility.Visible;
            text_lastname.Visibility = Visibility.Visible;

            textbox_email.Visibility = Visibility.Hidden;
            textbox_name.Visibility = Visibility.Hidden;
            textbox_lastname.Visibility = Visibility.Hidden;
            textbox_surname.Visibility = Visibility.Hidden;

            btn_save.Visibility = Visibility.Hidden;
            btn_cancel_redact.Visibility = Visibility.Hidden;
            btn_redact.Visibility = Visibility.Visible;
        }

        private void btn_redact_Click(object sender, RoutedEventArgs e)
        {
            ActivatingEditing();
        }

        private void btn_cancel_redact_Click(object sender, RoutedEventArgs e)
        {
            UpdatingElementsAfterEditing();
        }
    }
}
