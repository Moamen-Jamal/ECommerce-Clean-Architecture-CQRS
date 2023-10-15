using Ecommerce.Application.Persistence.Abstractions;
using FluentValidation;

namespace Ecommerce.Application.DTOs.EntitiesDTO.Product
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator(IProductRepository repository)
        {
            RuleFor(x => x.Name)
                 .NotNull()
                 .NotEmpty().WithMessage("{PropertyName} is Required !")
                 .MinimumLength(3).WithMessage("{PropertyName} limit with {ComparisonValue} charcture .")
                 .MaximumLength(100).WithMessage("{PropertyName} limit with {ComparisonValue} charcture .");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("{PropertyName} greater than  {ComparisonValue}");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("{PropertyName} greater than  {ComprisonValue}")
                .MustAsync(async (id, token) =>
                {
                    var categoryIdExist = await repository.IsCategoryExist(id);
                    return categoryIdExist;
                })
                .WithMessage("{PropertyName} does not exist ?");

        }
    }
}
