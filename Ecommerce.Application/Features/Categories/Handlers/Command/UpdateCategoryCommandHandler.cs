using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO.Category;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Models;
using Ecommerce.Application.Persistence.Abstractions;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BaseCommandResponse<object>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse<object>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse<object> response = new();

            try
            {
                // Check Validator
                var valiator = new CategoryValidator();
                var validatorResult = await valiator.ValidateAsync(request.CategoryDto);
                if (validatorResult.IsValid == false)
                {
                    response.Successed = false;
                    response.Message = "Failed while creating the category item";
                    ResponseMessageHelper.BadRequest(response.Message, response);
                }
                else
                {
                    var oldCategory = await _repository.GetByIdAsync(request.CategoryDto.Id);

                    if (oldCategory == null || oldCategory.Id <= 0)
                    {
                        response.Successed = false;
                        response.Message = "Category is Not Found";
                        ResponseMessageHelper.BadRequest(response.Message, response);
                        return response;
                    }

                    var res = _mapper.Map(request.CategoryDto, oldCategory);
                    response.Data = _repository.Update(res);
                    _unitOfWork.Commit();
                    response.Successed = true;
                    response.Message = "The category item is created successfully";
                }
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while creating the category item";
                ResponseMessageHelper.ServerError(response.Message, response);
            }

            return response;
        }
    }
}
