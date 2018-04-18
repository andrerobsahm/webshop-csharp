using System;
using NUnit.Framework;
using Webshop.Controllers;
using Webshop.Models;

namespace WebshopUnitTests
{
    public class Class1
    {

        [Test]
        public void GetCookie_Sets_Cookie_Given_Unset_Cookie_Key()
        {
            // Arrange

            CartController controller = new CartController();


            // Act
            string cookie = controller.GetOrCreateCartId("CartId");


            // Assert
            Assert.That(cookie, Is.EqualTo(controller.GetOrCreateCartId("cartId")));
        }

    }

}
