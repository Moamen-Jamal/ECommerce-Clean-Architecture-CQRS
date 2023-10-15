using Ecommerce.Application.Models;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Requests
{
    public class DeleteCategoryCommand:IRequest<BaseCommandResponse<object>>
    {
        public int Id { get; set; }
    }
}
