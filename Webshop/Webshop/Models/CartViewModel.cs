using System;
namespace Webshop.Models
{
    public class CartViewModel
    {
        public string Cart_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        }
}
