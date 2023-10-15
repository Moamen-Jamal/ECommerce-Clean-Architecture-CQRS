using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.DTOs.EntitiesDTO.Product;
using Ecommerce.Application.Features.Products.Requests;
using Ecommerce.Application.Infrastructure;
using Ecommerce.Application.Models;
using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Domain.Entities;
using MediatR;
using System.Net;

namespace Ecommerce.Application.Features.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseCommandResponse<object>>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, 
            IMapper mapper, IProductRepository repository
            , IEmailSender emailSender
            )
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
            _emailSender = emailSender;
        }
        public async Task<BaseCommandResponse<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //check Validator
            BaseCommandResponse<object> response = new();
            try
            {
                var validator = new ProductValidator(_repository);
                var validatorResult = await validator.ValidateAsync(request.ProductDto, cancellationToken);
                if (validatorResult.IsValid == false)
                {
                    response.Successed = false;
                    response.Message = "Failed while creating the Product item";
                    ResponseMessageHelper.BadRequest(response.Message, response);
                }
                else
                {
                    var product = _mapper.Map<Product>(request.ProductDto);
                    var createEntity = _repository.Create(product);
                    _unitOfWork.Commit();

                    var result =  _emailSender.SendEmail();
                    if (!result)
                    {
                        response.Message = "Failed to send an Email";
                        response.Successed = false;
                        response.Status = (int)HttpStatusCode.BadRequest;
                        return response;
                    }

                    response.Successed = true;
                    response.Message = "The Product item is created successfully";
                    response.Data = _mapper.Map<ProductDTO>(createEntity);
                }
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while creating the product item";
                ResponseMessageHelper.ServerError(response.Message, response);
            }
            
            return response;
        }
    }
}
