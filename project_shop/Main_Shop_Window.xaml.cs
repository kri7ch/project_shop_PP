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

namespace project_shop
{
    /// <summary>
    /// Логика взаимодействия для Main_Shop_Window.xaml
    /// </summary>
    public partial class Main_Shop_Window : Page
    {
        List<Product_class> list = new List<Product_class> { };
        public Main_Shop_Window()
        {
            InitializeComponent();
            Product_class item1 = new Product_class("Футболка Gucci", "Белая футболка из хлопка", "22 шт", "1999 руб");

            list = new List<Product_class> { item1 };
            lv_product.ItemsSource = list;
        }

    }
}
