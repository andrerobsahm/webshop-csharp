using System;
using System.Collections.Generic;
using Webshop.Models;

namespace Webshop.Repostories
{
    public interface IProductsRepository
    {
        List<ProductsViewModel> GetAll();
        ProductsViewModel Get(string Id);
    }

}
