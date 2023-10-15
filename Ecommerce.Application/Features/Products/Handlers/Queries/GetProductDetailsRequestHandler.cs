using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Features.Products.Requests;
using Ecommerce.Application.Persistence.Abstractions;
using MediatR;

namespace Ecommerce.Application.Features.Products.Handlers
{
    public class GetProductDetailsRequestHandler : IRequestHandler<GetProductDetailsRequest, ProductDTO>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductDetailsRequestHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProductDTO> Handle(GetProductDetailsRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            var response = _mapper.Map<ProductDTO>(product);
            return response;

        }
    }
}
