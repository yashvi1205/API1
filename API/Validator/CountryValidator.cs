using FluentValidation;
namespace API;

public class CountryValidator:AbstractValidator<CountryModel>
{
    public CountryValidator()
    {
        RuleFor(c => c.CountryName).NotNull().NotEmpty().WithMessage("Please enter the Name ");
    }
    
}