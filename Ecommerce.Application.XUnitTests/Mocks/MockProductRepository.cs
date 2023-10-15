using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerce.Application.XUnitTests.Mocks
{
    public static class MockProductRepository
    {
        public static Mock<IProductRepository> GetProductRepository()
        {
            #region MockData
            var products = new List<Product>
            {
                new Product{Id=1,Name="p1",Price=500,CategoryId = 1},
                new Product{Id=2,Name="p2",Price=700,CategoryId = 1},
            };
            #endregion

            #region object moqrepo
            //object moqrepo
            var moqrepo = new Mock<IProductRepository>();
            #endregion

            #region get all product
            //get all product
            moqrepo.Setup(x => x.GetAllAsync()).ReturnsAsync(products);
            #endregion

            #region get product by Id
            //get product by Id
            moqrepo.Setup(x => x.GetByIdAsync(products[0].Id)).ReturnsAsync(products[0]);
            #endregion

            #region create product
            // create product
            moqrepo.Setup(x => x.Create(It.IsAny<Product>())).Returns((Product product) =>
            {
                products.Add(product);
                return product;
            });
            #endregion

            #region update product
            // update product
            moqrepo.Setup(x => x.Update(It.IsAny<Product>())).Returns((Product product) =>
            {
                var oldProduct = products.Find(c => c.Id == product.Id);

                products.Remove(oldProduct);
                products.Add(product);
                return It.IsAny<Product>();
            });
            #endregion

            #region delete product
            // delete product
            moqrepo.Setup(x => x.DeleteById(products[0].Id)).Returns(() =>
            {
                var oldProduct = products.Find(c => c.Id == products[0].Id);

                products.Remove(oldProduct);
                return oldProduct;
            });
            #endregion

            return moqrepo;
        }
    }
}
