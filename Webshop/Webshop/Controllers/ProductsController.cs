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
using Webshop.Services.Implementations;
using Webshop.Repostories.Implementations;
using Webshop.Repostories;

namespace Webshop.Controllers

{
    public class ProductsController : Controller
    {
        
        private readonly ProductsService productsService;

        public ProductsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");

            this.productsService = new ProductsService(new ProductsRepository(connectionString));
        }


        public IActionResult Index()
        {
            var products = this.productsService.GetAll();
            return View(products);
        }

        public IActionResult Item(string Id)
        {
            var item = this.productsService.Get(Id);
            return View(item);
        }

    }
}