using Ecommerce.Application;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Features.Products.Requests;
using Ecommerce.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAllProduct
        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns name="result" type="BaseCommandResponse<object>"></returns>
        [HttpGet("GetAllProducts")]
        public async Task<BaseCommandResponse<object>> GetAllProducts()
        {
            BaseCommandResponse<object> result = new();

            try
            {
                var Products = await _mediator.Send(new GetAllProductsRequest());
                if (Products == null || Products.Count() == 0)
                {
                    result.Successed = false;

                    result.Message = "Products are not found";
                    ResponseMessageHelper.BadRequest(result.Message, result);
                    return result;
                }

                result.Successed = true;
                result.Data = Products;
            }
            catch
            {
                result.Successed = false;
                result.Message = "Something wrong has happened while retrieving the Products";
                ResponseMessageHelper.ServerError(result.Message, result);
            }

            return result;
        }
        #endregion

        #region GetProductById
        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <returns name="result" type="BaseCommandResponse<object>"></returns>
        [HttpGet("GetProductById/{id}")]
        public async Task<BaseCommandResponse<object>> GetProductById(int id)
        {
            BaseCommandResponse<object> result = new();

            try
            {
                if (id <= 0)
                {
                    result.Successed = false;
                    result.Message = "Id is not valid";
                    ResponseMessageHelper.BadRequest(result.Message, result);
                    return result;
                }
                var Product = await _mediator.Send(new GetProductDetailsRequest { Id = id });


                if (Product == null || Product?.Id <= 0)
                {
                    result.Successed = false;
                    result.Message = "The Product is not found.";
                    ResponseMessageHelper.BadRequest(result.Message, result);
                    return result;
                }

                result.Successed = true;
                result.Data = Product;
            }
            catch
            {
                result.Successed = false;
                result.Message = "Something wrong has happened while retrieving the Product";
                ResponseMessageHelper.ServerError(result.Message, result);

            }

            return result;

        }
        #endregion

        #region Add New Product
        /// <summary>
                /// Add New Product
                /// </summary>
                /// <returns name="response" type="BaseCommandResponse<object>"></returns>

        [HttpPost("AddNewProduct")]
        public async Task<BaseCommandResponse<object>> AddNewProduct([FromBody] ProductDTO productDto)
        {
            BaseCommandResponse<object> response = new();
            try
            {
                var createProductCommand = new CreateProductCommand { ProductDto = productDto };
                response = await _mediator.Send(createProductCommand);
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while retrieving the product";
                ResponseMessageHelper.ServerError(response.Message, response);
            }

            return response;
        }
        #endregion

        #region Update Product
        /// <summary>
        /// Update Exist Product
        /// </summary>
        /// <returns name="response" type="BaseCommandResponse<object>"></returns>
        [HttpPut("UpdateProduct")]
        public async Task<BaseCommandResponse<object>> UpdateProduct([FromBody] ProductDTO productDto)
        {
            BaseCommandResponse<object> response = new();
            try
            {
                var updateProductCommand = new UpdateProductCommand { ProductDto = productDto };
                response = await _mediator.Send(updateProductCommand);
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while retrieving the Product";
                ResponseMessageHelper.ServerError(response.Message, response);
            }
            return response;
        }
        #endregion

        #region Delete Product
        /// <summary>
        /// Delete Exist Product
        /// </summary>
        /// <returns name="response" type="BaseCommandResponse<object>"></returns>
        [HttpDelete("DeleteProductById/{id}")]
        public async Task<BaseCommandResponse<object>> DeleteProductById(int id)
        {
            BaseCommandResponse<object> response = new();
            try
            {
                if (id <= 0)
                {
                    response.Successed = false;
                    response.Message = "Id is not valid";
                    ResponseMessageHelper.BadRequest(response.Message, response);
                    return response;
                }
                var deleteCommand = new DeleteProductCommand { Id = id };
                response = await _mediator.Send(deleteCommand);
                response.Successed = true;
                response.Message = "Product has been deleted successfully";
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while deleting the Product";
                ResponseMessageHelper.ServerError(response.Message, response);

            }


            return response;
        }
        #endregion
    }

}