using Azure;
using Ecommerce.Application;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Features.Categories.Requests;
using Ecommerce.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAllCategories
        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns name="result" type="BaseCommandResponse<object>"></returns>
        [HttpGet("GetAllCategories")]
        public async Task<BaseCommandResponse<object>> GetAllCategories()
        {
            BaseCommandResponse<object> result = new();

            try
            {

                var categories = await _mediator.Send(new GetAllCategoriesRequest());
                if (categories == null || categories.Count() == 0)
                {
                    result.Successed = false;

                    result.Message = "Categories are not found";
                    ResponseMessageHelper.BadRequest(result.Message, result);
                    return result;
                }

                result.Successed = true;
                result.Data = categories;
            }
            catch
            {
                result.Successed = false;
                result.Message = "Something wrong has happened while retrieving the categories";
                ResponseMessageHelper.ServerError(result.Message, result);
            }

            return result;
        }
        #endregion


        #region GetCategoryById
        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <returns name="result" type="BaseCommandResponse<object>"></returns>
        [HttpGet("GetCategoryById/{id}")]
        public async Task<BaseCommandResponse<object>> GetCategoryById(int id)
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

                var category = await _mediator.Send(new GetCatgoryDetailsRequest { Id = id });

                if(category == null || category?.Id <= 0)
                {
                    result.Successed = false;
                    result.Message = "The category is not found.";
                    ResponseMessageHelper.BadRequest(result.Message, result);
                    return result;
                }

                result.Successed = true;
                result.Data = category;
            }
            catch
            {
                result.Successed = false;
                result.Message = "Something wrong has happened while retrieving the category";
                ResponseMessageHelper.ServerError(result.Message, result);

            }

            return result;
        }
        #endregion


        #region Add New Category
        /// <summary>
        /// Add New Category
        /// </summary>
        /// <returns name="response" type="BaseCommandResponse<object>"></returns>
        [HttpPost("AddNewCategory")]
        public async Task<BaseCommandResponse<object>> AddNewCategory([FromBody] CategoryDTO categoryDto)
        {
            BaseCommandResponse<object> response = new();
            try
            {
                var createCategoryCommand = new CreateCategoryCommand{ CategoryDto = categoryDto };
                response = await _mediator.Send(createCategoryCommand);
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while retrieving the category";
                ResponseMessageHelper.ServerError(response.Message, response);
            }
            
            return response;
        }
        #endregion

        #region Update Category
        /// <summary>
        /// Update Exist Category
        /// </summary>
        /// <returns name="response" type="BaseCommandResponse<object>"></returns>
        [HttpPut("UpdateCategory")]
        public async Task<BaseCommandResponse<object>> UpdateCategory([FromBody] CategoryDTO categoryDto)
        {
            BaseCommandResponse<object> response = new();
            try
            {
                var updateCategoryCommand = new UpdateCategoryCommand { CategoryDto = categoryDto };
                response = await _mediator.Send(updateCategoryCommand);
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while retrieving the category";
                ResponseMessageHelper.ServerError(response.Message, response);
            }
            return response;
        }
        #endregion

        #region Delete Category
        /// <summary>
        /// Delete Exist Category
        /// </summary>
        /// <returns name="response" type="BaseCommandResponse<object>"></returns>
        [HttpDelete("DeleteCategoryById/{id}")]
        public async Task<BaseCommandResponse<object>> Delete(int id)
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
                var deleteCommand = new DeleteCategoryCommand { Id = id };
                response = await _mediator.Send(deleteCommand);
                response.Successed = true;
                response.Message = "Category has been deleted successfully";
            }
            catch
            {
                response.Successed = false;
                response.Message = "Something wrong has happened while deleting the category";
                ResponseMessageHelper.ServerError(response.Message, response);

            }


            return response;
        }
        #endregion
    }
}
