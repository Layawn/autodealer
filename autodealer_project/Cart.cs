using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autodealer_project
{
    class Cart
    {
        private static int id { get; set; }
        private static string name { get; set; }
        private static int quantity { get; set; }
        private static int price { get; set; }
        public static void addToCart(int id_position, string name_position, int quantity_position, int price_position)
        {
            id = id_position;
            name = name_position;
            quantity = quantity_position;
            price = price_position;
        }
        public static int getId()
        {
            return id;
        }
        public static string getName()
        {
            return name;
        }
        public static int getQuantity()
        {
            return quantity;
        }
        public static int getPrice()
        {
            return price;
        }
    }
}
