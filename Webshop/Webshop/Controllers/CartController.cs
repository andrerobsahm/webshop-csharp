using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Webshop.Controllers
{
    public class CartController : Controller
    {
        private readonly string connectionString;

        public CartController(IConfiguration configuration)
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



        public IActionResult Index()
        {
            var cartId = GetOrCreateCartId();

            using (var connection = new MySqlConnection(this.connectionString))
            {

                var cart = connection.Query<CartViewModel>(
                    "SELECT cart.cart_id, sum(cart.quantity) " +
                    "AS quantity, cart.product_id, products.price, products.title " +
                    "FROM products " +
                    "INNER JOIN cart " +
                    "ON cart.product_id = products.id " +
                    "WHERE cart.cart_id = @cartId " +
                    "GROUP BY cart.product_id;",
                        new { cartId }).ToList();
                
                return View(cart);
            }
        }


        //Here we add products to the cart table in the database
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var cartId = GetOrCreateCartId();
            var quantity = 1;

            using (var connection = new MySqlConnection(this.connectionString))
            {
                var addToCart = "INSERT INTO Cart (product_id, cart_id, quantity) VALUES(@id, @cartId, @quantity)";
                connection.Execute(addToCart, new { id, cartId, quantity });
            }
            return RedirectToAction("Item", "Products", new { @id=id });
        }



        //Here we remove products from the cart table in the database
        [HttpPost]
        public IActionResult RemoveFromCart(int Id)
        {
            var cartId = GetOrCreateCartId();

            using (var connection = new MySqlConnection(this.connectionString))
            {
                var removeFromCart = "DELETE FROM Cart WHERE cart.cart_id = @cartId AND cart.product_id = @id LIMIT 1;";
                    connection.Execute(removeFromCart, new { Id, cartId });
            }
            return RedirectToAction("Index");
        }

    }
}
