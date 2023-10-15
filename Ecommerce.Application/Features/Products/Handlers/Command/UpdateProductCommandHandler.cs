using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.DTOs.EntitiesDTO.Product;
using Ecommerce.Application.Features.Products.Requests;
using Ecommerce.Application.Models;
using Ecommerce.Application.Persistence.Abstractions;
using MediatR;

namespace Ecommerce.Application.Features.Products.Handlers.Command
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseCommandResponse<object>>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse<object> response = new();

            try
            {
                // Check Validator
                var validator = new ProductValidator(_repository);
                var validatorResult = await validator.ValidateAsync(request.ProductDto, cancellationToken);
                if (validatorResult.IsValid == false)
                {
                    response.Successed = false;
                    response.Message = "Failed while creating the product item";
                    ResponseMessageHelper.BadRequest(response.Message, response);
                }
                else
                {
                    var oldProduct = await _repository.GetByIdAsync(request.ProductDto.Id);

                    if (oldProduct == null || oldProduct.Id <= 0)
                    {
                        response.Successed = false;
                        response.Message = "Product is Not Found";
                        ResponseMessageHelper.BadRequest(response.Message, response);
                        return response;
                    }

                    var res = _mapper.Map(request.ProductDto, oldProduct);
                    var update = _repository.Update(res);
                    response.Data = _mapper.Map<ProductDTO>(update);
                    _unitOfWork.Commit();
                    response.Successed = true;
                    response.Message = "The product item is created successfully";
                }
            }
                
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while creating the product item";
                ResponseMessageHelper.ServerError(response.Message, response);
            }
                
            //if (validatorResult.IsValid == false)
            //    throw new ValidationExecption(validatorResult);

            
            return response;
        }
    }
}
