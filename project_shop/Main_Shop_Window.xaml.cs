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
        string path = "C:\\Users\\rakhm\\OneDrive\\Рабочий стол\\Учет товаров\\Учет товаров.txt";
        public List<Products> all_product_json;
        public Main_Shop_Window()
        {
            InitializeComponent();
            string json = File.ReadAllText(path);
            List<Products> all_product_json = JsonConvert.DeserializeObject<List<Products>>(json);
            list_view_products.ItemsSource = all_product_json;
        }

    }
}
