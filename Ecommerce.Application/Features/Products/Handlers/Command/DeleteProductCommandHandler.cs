using Ecommerce.Application.Features.Products.Requests;
using Ecommerce.Application.Models;
using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Products.Handlers.Command
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseCommandResponse<object>>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<BaseCommandResponse<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse<object> response = new();
            try
            {
                var oldProduct = await _repository.GetByIdAsync(request.Id);

                if (oldProduct == null || oldProduct.Id <= 0)
                {
                    response.Successed = false;
                    response.Message = "Product is Not Found";
                    ResponseMessageHelper.BadRequest(response.Message, response);
                    return response;
                }

                _repository.DeleteById(oldProduct.Id);
                _unitOfWork.Commit();
                response.Successed = true;
                response.Message = "The product item is deleted successfully";
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while deleting the product item";
                ResponseMessageHelper.ServerError(response.Message, response);
            }
            return response;
            //return Unit.Value;
        }
    }
}
