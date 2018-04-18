﻿using System;
using System.Collections.Generic;
using Webshop.Models;

namespace Webshop.Models
{
    public class CheckoutViewModel
    {
        public string Cart_id { get; set; }
        public float Sum { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
    }
}
