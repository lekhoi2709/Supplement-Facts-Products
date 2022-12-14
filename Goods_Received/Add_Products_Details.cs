using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Goods_Received
{
    internal class Add_Products_Details
    {
        public string GoodReceivedId { get; set; }
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public double Total { get; set; }

        public Add_Products_Details(string goodReceivedId, string productName, string productID, int quantity, int price, double total)
        {
            GoodReceivedId = goodReceivedId;
            ProductName = productName;
            ProductID = productID;
            Quantity = quantity;
            Price = price;
            Total = total;
        }
    }
}
