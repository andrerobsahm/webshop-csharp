using System;
using System.Collections.Generic;
using Webshop.Models;

namespace Webshop.Models
{
    public class CheckoutViewModel
    {
        public List<CartViewModel> Cart { get; set; }

        public int Sum { get; set; }
    }
}
