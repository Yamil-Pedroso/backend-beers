using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("The name has to have more than 2 characters other less than 20 characters");
            RuleFor(x => x.BrandID).NotNull().WithMessage(x => "The brand is required");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage(x => "Error send with the brand's value");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage(x => "The {PropertyName} must to be greater than 0");
        }
    }
}
