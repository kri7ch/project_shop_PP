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
    /// Логика взаимодействия для Product_card.xaml
    /// </summary>
    public partial class Product_card : UserControl
    {
        public event RoutedEventHandler BuyButtonClicked;
        public Product_card()
        {
            InitializeComponent();
        }
        public void UpdateProductCount(int newCount)
        {
            CountTextBlock.Text = $"Количество: {newCount} шт.";
        }
        public void SetProductData(Products product)
        {
            NameTextBlock.Text = product.Name_Product;
            CountTextBlock.Text = $"В наличии: {product.Count_Product} шт.";
            PriceTextBlock.Text = $"Цена: {product.Price} руб.";
        }
        private void btn_buy_Click(object sender, RoutedEventArgs e)
        {
            BuyButtonClicked?.Invoke(this, e);
        }
    }
}
