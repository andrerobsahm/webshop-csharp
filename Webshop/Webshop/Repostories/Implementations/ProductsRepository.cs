using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Webshop.Models;

namespace Webshop.Repostories.Implementations
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly string connectionString;

        public ProductsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ProductsViewModel> GetAll()
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<ProductsViewModel>("Select * from Products").ToList();
            }
        }

        public ProductsViewModel Get(string Id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<ProductsViewModel>("Select * from Products where id = @id", new { Id });
            }

        }

    }
}
