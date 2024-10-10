using System;
using System.Collections.Generic;
using System.IO;
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
        public Product_card()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
