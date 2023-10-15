using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Models;
using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseCommandResponse<object>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<BaseCommandResponse<object>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse<object> response = new();
            try
            {
                //get category by id
                var oldCategory = await _repository.GetByIdAsync(request.Id);

                if (oldCategory == null || oldCategory.Id <= 0)
                {
                    response.Successed = false;
                    response.Message = "Category is Not Found";
                    ResponseMessageHelper.BadRequest(response.Message, response);
                    return response;
                }
                _repository.DeleteById(oldCategory.Id);
                _unitOfWork.Commit();
                response.Successed = true;
                response.Message = "The category item is deleted successfully";
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while deleting the category item";
                ResponseMessageHelper.ServerError(response.Message, response);
            }
            
            return response;
        }

    }
}
