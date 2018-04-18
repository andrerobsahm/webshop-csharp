using System;
namespace Webshop.Controllers
{
    public class OrderController
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int Sum { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
    }
}
