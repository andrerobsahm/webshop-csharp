using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Webshop.Models;
using MySql.Data.MySqlClient;

namespace Webshop.Controllers

{
    public class ProductsController : Controller
    {
        
        private readonly string connectionString;

        public ProductsController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
        }


        public IActionResult Index()
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var products = connection.Query<ProductsViewModel>("Select * from Products").ToList();
            
                if (products != null)
                {
                    return View(products);
                }
                else
                {
                    return NotFound();
                }
            }

        }

        public IActionResult Item(string Id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var item = connection.QuerySingleOrDefault<ProductsViewModel>("Select * from Products where id = @id", new { Id });
                if (item != null)
                {
                    return View(item);
                }
                else
                {
                    return NotFound("There is no such product.");
                }
            }   
        }

    }
}