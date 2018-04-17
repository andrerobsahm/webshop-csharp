using System;
using System.Collections.Generic;
using Webshop.Models;

namespace Webshop.Models
{
    public class CheckoutViewModel
    {
        public string Cart_id { get; set; }
        public float Sum { get; set; }
    }
}
