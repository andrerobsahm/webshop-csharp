using System;

namespace Webshop.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Image { get; set; }
    }
}
