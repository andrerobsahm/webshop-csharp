using System;
using Webshop.Repostories;
using Webshop.Repostories.Implementations;
using Webshop.Models;
using System.Collections.Generic;

namespace Webshop.Services.Implementations
{
    public class ProductsService
    {
        private readonly IProductsRepository productsRepository;


        public ProductsService(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public List<ProductsViewModel> GetAll()
        {
            return this.productsRepository.GetAll();
        }

        public ProductsViewModel Get(string Id)
        {
            return this.productsRepository.Get(Id);
        }
    }
}
