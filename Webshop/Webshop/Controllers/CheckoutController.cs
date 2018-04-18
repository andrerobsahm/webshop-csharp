using System;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly string connectionString;

        public CheckoutController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
        }


        //Generate a new GUID as cart Id where the products are stored
        private string GetOrCreateCartId()
        {
            if (this.Request.Cookies.ContainsKey("CartId"))
            {
                return this.Request.Cookies["CartId"];
            }

            var cartId = Guid.NewGuid().ToString();
            this.Response.Cookies.Append("CartId", cartId);

            return cartId;
        }



        //Here we select what we can see on the checkout page
        public IActionResult Index()
        {
            var cartId = GetOrCreateCartId();

            using (var connection = new MySqlConnection(this.connectionString))
            {
                var checkout = connection.Query<CheckoutViewModel>("SELECT cart.cart_id, sum(cart.quantity*products.price) " +
                    "AS sum FROM products " +
                    "JOIN cart " +
                    "ON cart.product_id = products.id " +
                    "WHERE cart.cart_id = @cartId;",
                    new { cartId }).ToList();


                return View(checkout);
            }

        }

        [HttpPost]
        public IActionResult AddToOrder(int sum, string name, string email, string address, string city, int zip)
        {
            var cartId = GetOrCreateCartId();

            using (var connection = new MySqlConnection(this.connectionString))
            {
                var addToOrder = "INSERT INTO orders(cart_id, sum, name, email, address, city, zip) VALUES(@cartId, @sum, @name, @email, @address, @city, @zip)";
                connection.Execute(addToOrder, new
                {
                    cartId = @cartId,
                    sum = @sum,
                    name = @name,
                    email = @email,
                    address = @address,
                    city = @city,
                    zip = @zip,
                });
            }
            return RedirectToAction("Index", "Home");

        }

    }
}
