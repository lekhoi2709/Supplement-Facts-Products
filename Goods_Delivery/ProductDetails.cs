using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods_Delivery
{
    internal class ProductDetails
    {
        public string DeliveryId { get; set; }
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public double Total { get; set; }

        public ProductDetails(string deliveryId, string productName, string productID, int quantity, int price, double total)
        {
            DeliveryId = deliveryId;
            ProductName = productName;
            ProductID = productID;
            Quantity = quantity;
            Price = price;
            Total = total;
        }
    }
}
