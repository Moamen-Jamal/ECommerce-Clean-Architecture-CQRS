using FluentValidation;

namespace Ecommerce.Application.DTOs.EntitiesDTO.Category
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is Required !")
                .MinimumLength(3).WithMessage("{PropertyName} limit with 3 charcture .")
                .MaximumLength(100).WithMessage("{PropertyName} limit with 100 charcture .");

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is Required !")
                .MinimumLength(3).WithMessage("{PropertyName} limit with 3 charcture .")
                .MaximumLength(300).WithMessage("{PropertyName} limit with 300 charcture .");
        }
    }
}
