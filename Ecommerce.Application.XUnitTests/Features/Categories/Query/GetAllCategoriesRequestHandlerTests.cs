using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Features.Categories.Handlers;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Application.XUnitTests.Mocks;
using Shouldly;

namespace Ecommerce.Application.XUnitTests.Features.Categories.Query
{
    public class GetAllCategoriesRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private GetAllCategoriesRequestHandler _handler { get; set; }

        private List<CategoryDTO> _result;
        private readonly Mock<ICategoryRepository> _moqrepo;
        public GetAllCategoriesRequestHandlerTests()
        {
            #region Mock Obj
            _moqrepo = MockCategoryRepository.GetCategoryRepository();
            #endregion

            #region configure mapper
            //configure mapper
            var mapperconfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperconfig.CreateMapper();
            #endregion

            #region Arrange
            _handler = new GetAllCategoriesRequestHandler(_moqrepo.Object, _mapper);
            #endregion

        }

        [Fact]

        public async Task Handle_ValidRequest_ReturnTypeObject()
        {
            #region Act
            _result = await _handler.Handle(new GetAllCategoriesRequest(), CancellationToken.None);
            #endregion

            #region Assert
            _result.ShouldBeOfType<List<CategoryDTO>>();
            #endregion
        }
        [Fact]
        public async Task Handle_ValidRequest_GetCategoryListCount()
        {
            #region Act
            _result = await _handler.Handle(new GetAllCategoriesRequest(), CancellationToken.None);
            #endregion

            #region Assert
            _result.Count.ShouldBeGreaterThanOrEqualTo(0);
            #endregion
        }
    }
}
