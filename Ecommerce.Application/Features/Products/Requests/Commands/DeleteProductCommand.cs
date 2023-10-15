using Ecommerce.Application.Models;
using MediatR;

namespace Ecommerce.Application.Features.Products.Requests
{
    public class DeleteProductCommand : IRequest<BaseCommandResponse<object>>
    {
        public int Id { get; set; }
    }
}
