using System;
using System.Collections.Generic;
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
                var addToOrder = "INSERT INTO orders(cart_id, sum, name, email, address, city, zip) VALUES(@cartId, @sum, @name, @email, @address, @city, @zip); SELECT last_insert_id();";
                var cart_id = connection.QuerySingleOrDefault<int>(addToOrder, new
                {
                    cartId = @cartId,
                    sum = @sum,
                    name = @name,
                    email = @email,
                    address = @address,
                    city = @city,
                    zip = @zip,
                });

                List<ProductsViewModel> cartItems = connection.Query<ProductsViewModel>("SELECT products.id FROM products JOIN cart ON cart.product_id = products.id WHERE cart_id = @cartId",
                                                                new { cartId = cartId }).ToList();

                foreach (Models.ProductsViewModel item in cartItems)
                {
                    connection.Execute("INSERT INTO OrderedProducts(cart_id, product_id) VALUES (@cartId, @product_id)",
                                       new { cartId = cart_id, product_id = item.Id});
                }

                connection.Execute("DELETE FROM Cart WHERE cart_id = @cartId",
                                   new { cartId = cartId });
            }
            this.Response.Cookies.Delete("cartId");
            return RedirectToAction("Index", "Home");

        }
    }
}
