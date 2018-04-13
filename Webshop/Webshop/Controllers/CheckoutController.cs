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


        public IActionResult Index()
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var checkout = connection.Query<CheckoutViewModel>("Select * from Cart").ToList();

                return View(checkout);
                }

        }


    }
}
