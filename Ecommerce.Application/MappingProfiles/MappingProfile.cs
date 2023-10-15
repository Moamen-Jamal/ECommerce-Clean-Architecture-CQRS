using AutoMapper;
using Ecommerce.Application.DTOs.EntitiesDTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            // Configure Category Automapper 
            CreateMap<Category, CategoryDTO>().ReverseMap();

            // Configure Product Automapper 
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
