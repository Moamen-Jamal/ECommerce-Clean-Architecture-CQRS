using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO.Category;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Models;
using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseCommandResponse<object>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ICategoryRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse<object>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse<object> response = new ();

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
                    var category = _mapper.Map<Category>(request.CategoryDto);
                    var categoryEntity =  _repository.Create(category);
                     _unitOfWork.Commit();
                    response.Successed = true;
                    response.Message = "The category item is created successfully";
                    response.Data = categoryEntity;
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
