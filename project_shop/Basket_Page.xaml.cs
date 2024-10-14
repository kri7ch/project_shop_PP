﻿using Newtonsoft.Json;
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

namespace project_shop
{
    /// <summary>
    /// Логика взаимодействия для Basket_Page.xaml
    /// </summary>
    public partial class Basket_Page : Page
    {
        int totalprice = 0;
        private int currentUserId;
        public Basket_Page(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            LoadSavedProducts();
            blockbtn();
        }
        private void blockbtn() 
        { 
            if (totalprice == 0) 
            { 
                btn_buy.IsEnabled = false;
            }
        }
        private void LoadSavedProducts()
        {
            string path = "korzina.json";
            if (File.Exists(path))
            {
                
                string json = File.ReadAllText(path);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    List<Products> savedProducts = JsonConvert.DeserializeObject<List<Products>>(json);
                    foreach (var product in savedProducts)
                    {
                        string priceText = product.Price;
                        string priceNumberString = priceText.Split(' ')[1];

                        if (int.TryParse(priceNumberString, out int price))
                        {
                            int totalPriceProduct = price * product.Count_Product;
                            basketList.Items.Add($"{product.Name_Product} \nКоличество: {product.Count_Product} шт. \nЦена: {totalPriceProduct} руб.");
                        }
                        else
                        {
                            MessageBox.Show($"Временная неполадка с ценником");
                        }
                    }
                }
                
                foreach (var item in basketList.Items)
                {
                    string[] parts = item.ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 2)
                    {
                        string priceText = parts[2];
                        string priceNumberString = priceText.Split(' ')[1];
                        if (int.TryParse(priceNumberString, out int price))
                        {
                            totalprice += price;
                        }
                    }
                }
                totalPrice.Content = totalprice.ToString() + " руб.";
            }
            else
            {
                MessageBox.Show("Корзина пуста!");
            }
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

        private void Checkout()
        {
            int totalPriceValue = 0;
            foreach (var item in basketList.Items)
            {
                string[] parts = item.ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 2)
                {
                    string priceText = parts[2];
                    string priceNumberString = priceText.Split(' ')[1];
                    if (int.TryParse(priceNumberString, out int price))
                    {
                        totalPriceValue += price;
                    }
                }
            }
            var result = MessageBox.Show($"Оформить заказ - {totalPriceValue} руб. ?", "Оформление заказа", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ClearBasket();
                MainMenu_Page();
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
        private void btn_buy_Click(object sender, RoutedEventArgs e)
        {
            Checkout();
        }
    }
}