using System;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using Webshop.Services.Implementations;
using Webshop.Repostories;
using Webshop.Repostories.Implementations;
using Webshop.Models;

namespace Webshop.UnitTests.Services
{
    public class ProductsServiceTest
    {
        private ProductsService productsService;
        private IProductsRepository productsRepository;

        [SetUp]
        public void SetUp()
        {
            this.productsRepository = A.Fake<IProductsRepository>();
            this.productsService = new ProductsService(this.productsRepository);
        }

        [Test]
        public void GetAll_ReturnsExpectedProducts()
        {
            // Arrange
            var products = new List<ProductsViewModel>
            {
                new ProductsViewModel()
            };

            A.CallTo(() => this.productsRepository.GetAll()).Returns(products);

            // Act
            var result = this.productsService.GetAll();

            // Assert
            Assert.That(result, Is.EqualTo(products));
        }

    }
}
