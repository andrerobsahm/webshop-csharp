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
                var products = connection.Query<ProductViewModel>("Select * from Products").ToList();
            
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

        public IActionResult SingleProduct(string id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var singleproduct = connection.QuerySingleOrDefault<ProductViewModel>("Select * from Products where id = @id", new { id });
                if (singleproduct != null)
                {
                    return View(singleproduct);
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }
}