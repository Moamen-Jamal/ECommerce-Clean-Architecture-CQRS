using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Models;
using Ecommerce.Application.Persistence.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Application.Features.Categories.Handlers;

public class GetAllCategoriesRequestHandler : IRequestHandler<GetAllCategoriesRequest, List<CategoryDTO>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCategoriesRequestHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDTO>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllAsync();

        return categories .Count() > 0? _mapper.Map<List<CategoryDTO>>(categories): new List<CategoryDTO>();
    }
}
