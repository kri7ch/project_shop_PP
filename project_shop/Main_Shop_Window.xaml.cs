using Newtonsoft.Json;
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
    /// Логика взаимодействия для Main_Shop_Window.xaml
    /// </summary>
    public partial class Main_Shop_Window : Page
    {
        private int currentUserId;
        public Main_Shop_Window(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            LoadProducts();
        }
        private void LoadProducts()
        {
            string path = "products.json";
            string json = File.ReadAllText(path);
            List<Products> all_product_json = JsonConvert.DeserializeObject<List<Products>>(json);

            foreach (var product in all_product_json)
            {
                var productControl = new Product_card();
                productControl.SetProductData(product);
                productControl.UpdateProductCount(product.Count_Product);

                productControl.BuyButtonClicked += ProductControl_BuyButtonClicked;

                list_view_products.Items.Add(productControl);
            }
        }

        private void ProductControl_BuyButtonClicked(object sender, RoutedEventArgs e)
        {
            var productControl = sender as Product_card;
            if (productControl != null)
            {
                string productName = productControl.NameTextBlock.Text;
                string productPrice = productControl.PriceTextBlock.Text;

                var product = GetProductByName(productName);
                if (product != null && product.Count_Product > 0)
                {
                    SaveProductToJson(productName, productPrice);
                    product.Count_Product--;
                    productControl.UpdateProductCount(product.Count_Product);
                    UpdateProductInFile(product);
                }
                else
                {
                    MessageBox.Show("Товар закончился.");
                }
            }
        }
        private Products GetProductByName(string productName)
        {
            string path = "products.json";
            string json = File.ReadAllText(path);
            List<Products> all_product_json = JsonConvert.DeserializeObject<List<Products>>(json);
            return all_product_json.FirstOrDefault(p => p.Name_Product == productName);
        }

        private void UpdateProductInFile(Products product)
        {
            string path = "products.json";
            string json = File.ReadAllText(path);
            List<Products> all_product_json = JsonConvert.DeserializeObject<List<Products>>(json);

            var existingProduct = all_product_json.FirstOrDefault(p => p.Name_Product == product.Name_Product);
            if (existingProduct != null)
            {
                existingProduct.Count_Product = product.Count_Product;
            }

            string newJson = JsonConvert.SerializeObject(all_product_json, Formatting.Indented);
            File.WriteAllText(path, newJson);
        }
        private void SaveProductToJson(string productName, string productPrice)
        {
            var product = new Products { Name_Product = productName, Price = productPrice, Count_Product = 1 };
            string path = "korzina.json";

            List<Products> savedProducts;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                savedProducts = JsonConvert.DeserializeObject<List<Products>>(json) ?? new List<Products>();
            }
            else
            {
                savedProducts = new List<Products>();
            }

            var existingProduct = savedProducts.FirstOrDefault(p => p.Name_Product == productName);

            if (existingProduct != null)
            {
                existingProduct.Count_Product++;
            }
            else
            {
                savedProducts.Add(product);
            }

            string newJson = JsonConvert.SerializeObject(savedProducts, Formatting.Indented);
            File.WriteAllText(path, newJson);
        }
        public void Profile_Page()
        {
            var window = Window.GetWindow(this);
            window.Title = "Профиль";
            NavigationService.Navigate(new Profile_Page(currentUserId));
        }
        private void Hyperlink_Click_Profile(object sender, RoutedEventArgs e)
        {
            Profile_Page();
        }
        private void btn_enter_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
