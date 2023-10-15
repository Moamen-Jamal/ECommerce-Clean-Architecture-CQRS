using Ecommerce.Application.DTOs.EntitiesDTO;
using MediatR;

namespace Ecommerce.Application.Features.Products.Requests
{
    public class GetProductDetailsRequest : IRequest<ProductDTO>
    {
        public int Id { get; set; }
    }
}
