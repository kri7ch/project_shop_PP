using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


namespace project_shop
{
    /// <summary>
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class Regist : Page
    {
        public Regist()
        {
            InitializeComponent();
        }
        public void Entry_window()
        {
            var window = Window.GetWindow(this);
            window.Title = "Авторизация";
            NavigationService.Navigate(new Aut());
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Entry_window();
        }
        private bool Checking_for_completion()
        {
            bool isEmpty = false;

            List<TextBox> textBoxes = new List<TextBox>
            {
                textbox_surname,
                textbox_name,
                textbox_email,
            };

            List<PasswordBox> passwordBoxes = new List<PasswordBox>
            {
                textbox_password,
                textbox_password_2
            };
            List<TextBox> tb_pass = new List<TextBox>
            {
                textbox_show_password,
                textbox_show_password_2,
            };

            foreach (TextBox textBox in textBoxes)
            {
                if (textBox.Text == "")
                {
                    isEmpty = true;
                }
            }

            foreach (PasswordBox passwordBox in passwordBoxes)
            {
                if (passwordBox.Password == "")
                {
                    foreach (TextBox tb_password in tb_pass)
                    {
                        if (tb_password.Text == "")
                        {
                            isEmpty = true;
                        }
                        else
                        {
                            isEmpty = false;
                        }
                    }
                }
            }

            if (isEmpty)
            {
                MessageBox.Show("Заполнены не все поля! Заполните поля и возвращайтесь вновь", "Предупреждение: Пустые поля", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return isEmpty;
        }

        public void SaveUser(User user)
        {
            List<User> users = new List<User>();

            if (File.Exists("users.json"))
            {
                string json = File.ReadAllText("users.json");
                users = JsonConvert.DeserializeObject<List<User>>(json);

                if (users.Any(u => u.Email == user.Email))
                {
                    MessageBox.Show("Пользователь с такой почтой уже существует!", "Предупреждение: Сущестующий адрес", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }
            users.Add(user);
            string newJson = JsonConvert.SerializeObject(users);
            File.WriteAllText("users.json", newJson);
            MessageBox.Show("Вы успешно зарегестрировались!", "Успешная регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
            Entry_window();
        }
        private bool Checking_mail()
        {
            bool isEmpty = false;
            if (!Regex.IsMatch(textbox_email.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$") || textbox_email.Text.Count(c => c == '@') != 1 || textbox_email.Text.Count(c => c == '.') != 1 || textbox_email.Text.Contains(".."))
            {
                isEmpty = true;
            }
            if (isEmpty)
            {
                MessageBox.Show("Пожалуйста, введите правильный адрес электронной почты.", "Предупреждение: Неверный адрес почты", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            return isEmpty;
        }
        private bool Password_verification()
        {
            bool isEmpty = false;
            if (checkbox_password.IsChecked == true)
            {
                if (!Regex.IsMatch(textbox_show_password.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$"))
                {
                    isEmpty = true;
                }
            }
            else
            {
                if (!Regex.IsMatch(textbox_password.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$"))
                {
                    isEmpty = true;
                }
            }
            if (isEmpty)
            {
                MessageBox.Show("Пароль слишком простой. Пароль должен содержать минимум одну заглавную букву, одну маленькую букву, одну цифру и быть не менее 6 символов.", "Предупреждение: неверный пароль", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            return isEmpty;
        }
        private bool Checking_for_password_approval()
        {
            bool isEmpty = false;
            if (checkbox_password.IsChecked == true)
            {
                if (checkbox_password_2.IsChecked == true)
                {
                    if (textbox_show_password.Text != textbox_show_password_2.Text)
                    {
                        isEmpty = true;
                    }
                }
                else
                {
                    if (textbox_show_password.Text != textbox_password_2.Password)
                    {
                        isEmpty = true;
                    }
                }
            }
            if (checkbox_password_2.IsChecked == true)
            {
                if (checkbox_password.IsChecked == true)
                {
                    if (textbox_show_password.Text != textbox_show_password_2.Text)
                    {
                        isEmpty = true;
                    }
                }
            }
            else
            {
                if (textbox_password.Password != textbox_password_2.Password)
                {
                    isEmpty = true;
                }
            }
            if (isEmpty)
            {
                MessageBox.Show("Пароли не совпадают, повторите ввод пароля!", "Предупреждение: не совпадают пароли", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                textbox_password_2.Clear();
            }
            return isEmpty;
        }
        public void Save_in_File()
        {
            if (Checking_for_completion() || Checking_mail() || Password_verification() || Checking_for_password_approval())
            {
                return;
            }
            User newUser = new User
            {
                Id = new Random().Next().ToString(),
                Email = textbox_email.Text,
                Surname = textbox_surname.Text,
                Name = textbox_name.Text,
                Password = textbox_password.Password,
                Middle_Name = textbox_middle_name.Text,
            };
            SaveUser(newUser);
        }
        private void btn_enter_Click(object sender, RoutedEventArgs e)
        {
            Save_in_File();
        }

        private void textbox_email_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"\p{IsCyrillic}"))
            {
                e.Handled = true;
            }
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
        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void textbox_password_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
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
        public void Remove_password_box_2()
        {
            textbox_password_2.Visibility = Visibility.Hidden;
            textbox_show_password_2.Visibility = Visibility.Visible;
        }
        public void Restore_password_box_2()
        {
            textbox_show_password_2.Visibility = Visibility.Hidden;
            textbox_password_2.Visibility = Visibility.Visible;
        }
        public void Checked_Pass_2()
        {
            if (checkbox_password_2.IsChecked == true)
            {
                textbox_show_password_2.Text = textbox_password_2.Password;
                Remove_password_box_2();
                textbox_show_password_2.Focus();
                textbox_show_password_2.Select(textbox_show_password_2.Text.Length, 0);
                if (checkbox_password_2.IsChecked == false)
                {
                    textbox_password_2.Password = textbox_show_password_2.Text;
                    Restore_password_box_2();
                    textbox_password_2.Focus();
                }
            }
        }
        public void Unchecked_Pass_2()
        {
            if (checkbox_password_2.IsChecked == false)
            {
                textbox_password_2.Password = textbox_show_password_2.Text;
                Restore_password_box_2();
                textbox_password_2.Focus();
                if (checkbox_password_2.IsChecked == true)
                {
                    textbox_show_password_2.Text = textbox_password_2.Password;
                    Remove_password_box_2();
                    textbox_show_password_2.Focus();
                    textbox_show_password_2.Select(textbox_show_password_2.Text.Length, 0);
                }
            }
        }
        private void checkbox_password_Checked_2(object sender, RoutedEventArgs e)
        {
            Checked_Pass_2();
        }

        private void checkbox_password_Unchecked_2(object sender, RoutedEventArgs e)
        {
            Unchecked_Pass_2();
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

        private void PasswordBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_2.Visibility = Visibility.Visible;
        }

        private void PasswordBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_2.Visibility = Visibility.Hidden;
        }
        private void textbox_otch_GotFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_5.Visibility = Visibility.Visible;
        }

        private void textbox_otch_LostFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_5.Visibility = Visibility.Hidden;
        }
        private void textbox_name_GotFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_4.Visibility = Visibility.Visible;
        }

        private void textbox_name_LostFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_4.Visibility = Visibility.Hidden;
        }
        private void textbox_surname_GotFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_3.Visibility = Visibility.Visible;
        }

        private void textbox_surname_LostFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_3.Visibility = Visibility.Hidden;
        }

        private void textbox_show_password_GotFocus(object sender, RoutedEventArgs e)
        {
            Image_Password.Visibility = Visibility.Visible;
        }

        private void textbox_show_password_LostFocus(object sender, RoutedEventArgs e)
        {
            Image_Password.Visibility = Visibility.Hidden;
        }
        private void textbox_show_password_2_GotFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_2.Visibility = Visibility.Visible;
        }

        private void textbox_show_password_2_LostFocus(object sender, RoutedEventArgs e)
        {
            Image_Password_2.Visibility = Visibility.Hidden;
        }
    }
}
