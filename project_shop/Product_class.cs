using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace project_shop
{
    internal class Product_class
    {
        public string Title_Product { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }

        public Product_class(string title, string description, string quantity, string price) 
        { 
            Title_Product = title;
            Description = description;
            Quantity = quantity;
            Price = price;
        }
    }
}
