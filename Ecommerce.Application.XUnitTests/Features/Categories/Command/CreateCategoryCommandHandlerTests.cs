using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Features.Categories.Handlers;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Models;
using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Application.XUnitTests.Mocks;
using Ecommerce.Domain.Entities;
using Shouldly;
using System.Net;

namespace Ecommerce.Application.XUnitTests.Features.Categories.Command
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private CreateCategoryCommandHandler _handler { get; set; }
        private CreateCategoryCommand _request { get; set; }
        private readonly Mock<ICategoryRepository> _moqrepo;
        private readonly Mock<IUnitOfWork> _moqUnitOfWork;
        private readonly CategoryDTO _categoryDto;
        public CreateCategoryCommandHandlerTests()
        {
            #region Mock Objs
            _moqrepo = MockCategoryRepository.GetCategoryRepository();
            _moqUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            #endregion

            #region Mapper
            //configure mapper
            var mapperconfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperconfig.CreateMapper();
            #endregion

            #region categoryDto
            _categoryDto = new CategoryDTO
            {
                Id = 0,
                Name = "cat_four",
                Description = "Cat_Desc"
            };
            #endregion

            #region Arrange
            // Arrange

            _handler = new CreateCategoryCommandHandler(_moqUnitOfWork.Object, _mapper, _moqrepo.Object);

            _request = new CreateCategoryCommand
            {
                CategoryDto = _categoryDto
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
            Assert.Equal("The category item is created successfully", response.Message);

            // Shouldly
            response.Successed.ShouldBeTrue();
            response.Message.ShouldBe("The category item is created successfully");
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

        [Fact]
        public async Task Handle_ValidRequest_CheckReturnObjectValue()
        {
            #region Arrange
            // Arrange

            Category category = new Category();
            category = _mapper.Map<Category>(_request.CategoryDto);
            #endregion

            #region Act
            // Act
            var response = await _handler.Handle(_request, CancellationToken.None);
            var Results = (Category)response.Data;

            #endregion

            #region Assert
            Assert.Equal(category.Name, Results.Name);
            #endregion
        }


        [Theory]
        [InlineData("ed","is")]
        public async Task Handle_InValidRequest_CheckNameDescPropsValue(string name, string desc)
        {
            #region Arrange
            // Arrange

            _request.CategoryDto.Name = name;
            _request.CategoryDto.Description = desc;
            #endregion

            #region Act
            // Act
            var response = await _handler.Handle(_request, CancellationToken.None);

            #endregion

            #region Assert
            response.Successed.ShouldBeFalse();
            response.Message.ShouldBe("Failed while creating the category item");
            response.Status.ShouldBe((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Theory]
        [InlineData(-1)]
        public async Task Handle_InValidRequest_CheckIdNegativeValue(int id)
        {
            #region Arrange
            // Arrange

            _request.CategoryDto.Id = id;
            #endregion

            #region Act
            // Act
            var response = await _handler.Handle(_request, CancellationToken.None);

            #endregion

            #region Assert
            response.Successed.ShouldBeFalse();
            response.Message.ShouldBe("Failed while creating the category item");
            response.Status.ShouldBe((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Handle_InvalidObjinputToRepo_HandleNullException()
        {
            #region Arrange
            // Arrange

            var handler = new CreateCategoryCommandHandler(_moqUnitOfWork.Object, null, _moqrepo.Object);
            #endregion

            #region Act
            // Act
            var response = await handler.Handle(_request, CancellationToken.None);

            #endregion

            #region Assert
            Assert.False(response.Successed);
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.Status);
            Assert.Equal("Something wrong has happened while creating the category item", response.Message);
            #endregion
        }

    }
}
