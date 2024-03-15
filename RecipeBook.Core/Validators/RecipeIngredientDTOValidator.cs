using FluentValidation;
using RecipeBook.Core.DTOs;
using RecipeBook.Core.Enums;

namespace RecipeBook.Core.Validators
{
    public class RecipeIngredientDTOValidator : AbstractValidator<RecipeIngredientDTO>
    {
        public RecipeIngredientDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ingredient name is required.")
                .MaximumLength(100).WithMessage("Ingredient name must not exceed 100 characters.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.MeasuringUnit)
                .IsInEnum().WithMessage("Invalid measuring unit.");
        }
    }
}
