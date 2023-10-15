using Ecommerce.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Application
{
    public static class ResponseMessageHelper
    {
        public static JsonResult BadRequest(string errorMessages, BaseCommandResponse<object> response)
        {
            response.Status = (int)HttpStatusCode.BadRequest;
            response.Message = errorMessages;

            return new JsonResult(response) { StatusCode = response.Status };
        }

        public static JsonResult ServerError(string errorMessages, BaseCommandResponse<object> response)
        {
            response.Status = (int)HttpStatusCode.InternalServerError;
            response.Message = errorMessages;

            return new JsonResult(response) { StatusCode = response.Status };
        }
    }
}
