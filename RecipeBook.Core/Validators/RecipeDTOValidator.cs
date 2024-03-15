using FluentValidation;
using RecipeBook.Core.DTOs;
using RecipeBook.Core.Enums;
using System.Linq;

namespace RecipeBook.Core.Validators
{
    public class RecipeDTOValidator : AbstractValidator<RecipeDTO>
    {
        public RecipeDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Recipe name is required.")
                .MaximumLength(100).WithMessage("Recipe name must not exceed 100 characters.");

            RuleFor(x => x.Instructions)
                .MaximumLength(1000).WithMessage("Instructions must not exceed 1000 characters.");

            RuleFor(x => x.Ingredients)
                .NotEmpty().WithMessage("Recipe must have ingredients.")
                .ForEach(item =>
                {
                    item.SetValidator(new RecipeIngredientDTOValidator());
                });
        }
    }
}
