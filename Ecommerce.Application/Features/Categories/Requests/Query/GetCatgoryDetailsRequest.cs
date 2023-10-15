﻿using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Application.Models;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Requests
{
    public class GetCatgoryDetailsRequest :IRequest<CategoryDTO>
    {
        public int Id { get; set; }
    }
}
