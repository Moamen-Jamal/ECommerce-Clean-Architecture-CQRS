using Ecommerce.Application.DTOs.EntitiesDTO;
using MediatR;

namespace Ecommerce.Application.Features.Products.Requests
{
    public class GetAllProductsRequest : IRequest<List<ProductDTO>>
    {
    }
}
