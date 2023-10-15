using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Models;
using MediatR;

namespace Ecommerce.Application.Features.Products.Requests
{
    public class UpdateProductCommand : IRequest<BaseCommandResponse<object>>
    {
        public ProductDTO ProductDto { get; set; }
    }
}
