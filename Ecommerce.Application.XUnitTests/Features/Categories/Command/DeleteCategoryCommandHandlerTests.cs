using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Features.Categories.Handlers;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Models;
using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Application.XUnitTests.Mocks;
using Shouldly;
using System.Net;

namespace Ecommerce.Application.XUnitTests.Features.Categories
{
    public class DeleteCategoryCommandHandlerTests
    {
        private DeleteCategoryCommandHandler _handler { get; set; }
        private DeleteCategoryCommand _request { get; set; }
        private readonly Mock<ICategoryRepository> _moqrepo;
        private readonly Mock<IUnitOfWork> _moqUnitOfWork;

        public DeleteCategoryCommandHandlerTests()
        {
            #region Mock Objs
            _moqrepo = MockCategoryRepository.GetCategoryRepository();
            _moqUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            #endregion

            #region Arrange
            // Arrange

            _handler = new DeleteCategoryCommandHandler(_moqUnitOfWork.Object, _moqrepo.Object);

            var categories = _moqrepo.Object.GetAllAsync();
            _request = new DeleteCategoryCommand
            {
                Id = categories.Result.Select(i => i.Id).FirstOrDefault()
            };

            #endregion
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {

            #region Act
            // Act
            var response = await _handler.Handle(_request, CancellationToken.None);
            #endregion

            #region Assert
            // Assert
            Assert.True(response.Successed);
            Assert.Equal("The category item is deleted successfully", response.Message);

            // Shouldly
            response.Successed.ShouldBeTrue();
            response.Message.ShouldBe("The category item is deleted successfully");
            #endregion
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnTypeObject()
        {

            #region Act
            // Act
            var response = await _handler.Handle(_request, CancellationToken.None);
            #endregion


            #region Assert
            Assert.IsType<BaseCommandResponse<object>>(response);
            response.ShouldBeOfType<BaseCommandResponse<object>>();
            #endregion

        }

        [Theory]
        [InlineData(0)]
        public async Task Handle_InValidRequest_CheckNotFoundValue(int id)
        {
            #region Arrange
            // Arrange

            _request.Id = id;
            #endregion

            #region Act
            // Act
            var response = await _handler.Handle(_request, CancellationToken.None);

            #endregion

            #region Assert
            response.Successed.ShouldBeFalse();
            response.Message.ShouldBe("Category is Not Found");
            response.Status.ShouldBe((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Handle_InvalidObjinputToRepo_HandleNullException()
        {
            #region Arrange
            // Arrange

            var handler = new DeleteCategoryCommandHandler(_moqUnitOfWork.Object, null);
            #endregion

            #region Act
            // Act
            var response = await handler.Handle(_request, CancellationToken.None);

            #endregion

            #region Assert
            Assert.False(response.Successed);
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.Status);
            Assert.Equal("Something wrong has happened while deleting the category item", response.Message);
            #endregion
        }

    }
}
