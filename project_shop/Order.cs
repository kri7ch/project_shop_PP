using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_shop
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderAmount { get; set; }
        public string OrderDate { get; set; }

        public Order(int orderId, int orderAmount, string orderDate)
        {
            OrderId = orderId;
            OrderAmount = orderAmount;
            OrderDate = orderDate;
        }
    }
}
