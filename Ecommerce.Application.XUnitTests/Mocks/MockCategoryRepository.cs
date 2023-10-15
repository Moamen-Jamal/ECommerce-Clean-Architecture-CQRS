global using Moq;
using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Ecommerce.Application.XUnitTests.Mocks
{
    public static class MockCategoryRepository
    {
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            #region MockData
            var categories = new List<Category>
            {
                new Category{Id=1,Name="cat_one",Description="des_one"},
                new Category{Id=2,Name="cat_tow",Description="des_tow"},
                new Category{Id=3,Name="cat_three",Description="des_three"},
            };
            #endregion

            #region objects moq
            //object moq
            var moqrepo = new Mock<ICategoryRepository>();
            var entityEntryMock = new Mock<EntityEntry<Category>>();
            #endregion

            #region get all category
            //get all category
            moqrepo.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);
            #endregion

            #region get category by Id
            //get category by Id
            moqrepo.Setup(x => x.GetByIdAsync(categories[0].Id)).ReturnsAsync(categories[0]);
            #endregion

            #region create category
            // create category
            moqrepo.Setup(x => x.Create(It.IsAny<Category>())).Returns((Category category) =>
            {
                
                categories.Add(category);
                return category;
            });
            #endregion

            #region update category
            // update category
            moqrepo.Setup(x => x.Update(It.IsAny<Category>())).Returns((Category category) =>
            {
                var oldCategory = categories.Find(c => c.Id == category.Id);

                categories.Remove(oldCategory);
                categories.Add(category);
                return category;
            });
            #endregion

            #region delete category
            // delete category
            moqrepo.Setup(x => x.DeleteById(categories[0].Id)).Returns(() =>
            {
                var oldCategory = categories.Find(c => c.Id == categories[0].Id);

                categories.Remove(oldCategory);
                return oldCategory;
            });
            #endregion

            return moqrepo;
        }
    }
}
