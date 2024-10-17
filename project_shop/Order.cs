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
        public DateTime OrderDate { get; set; }

        public Order(int orderId, int orderAmount, DateTime orderDate)
        {
            OrderId = orderId;
            OrderAmount = orderAmount;
            OrderDate = orderDate;
        }
    }
}
