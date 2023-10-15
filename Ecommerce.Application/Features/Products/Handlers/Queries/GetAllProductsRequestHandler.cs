using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Features.Products.Requests;
using Ecommerce.Application.Persistence.Abstractions;
using MediatR;

namespace Ecommerce.Application.Features.Products.Handlers
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, List<ProductDTO>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductsRequestHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<ProductDTO>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            return products.Count() > 0 ? _mapper.Map<List<ProductDTO>>(products) : new List<ProductDTO>();
        }
       
    }
}
