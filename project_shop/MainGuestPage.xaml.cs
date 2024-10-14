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
    /// Логика взаимодействия для MainGuestPage.xaml
    /// </summary>
    public partial class MainGuestPage : Page
    {
        public MainGuestPage()
        {
            InitializeComponent();
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

                list_view_products.Items.Add(productControl);
            }
        }

        private void btn_enter_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Registration_Page()
        {
            var window = Window.GetWindow(this);
            window.Title = "Регистрация";
            NavigationService.Navigate(new Regist());
        }

        private void Hyperlink_Click_Reg(object sender, RoutedEventArgs e)
        {
            Registration_Page();
        }
    }
}
