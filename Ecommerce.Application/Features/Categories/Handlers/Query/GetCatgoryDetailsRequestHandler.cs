using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Persistence.Abstractions;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Handlers
{
    public class GetCatgoryDetailsRequestHandler : IRequestHandler<GetCatgoryDetailsRequest, CategoryDTO>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCatgoryDetailsRequestHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<CategoryDTO> Handle(GetCatgoryDetailsRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<CategoryDTO>(category);

        }
    }
}
