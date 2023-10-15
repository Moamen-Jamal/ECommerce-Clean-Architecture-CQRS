using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Models;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Requests
{
    public class CreateCategoryCommand: IRequest<BaseCommandResponse<object>>
    {
        public CategoryDTO CategoryDto { get; set; }
    }
}
