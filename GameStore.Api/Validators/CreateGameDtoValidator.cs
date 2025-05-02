using FluentValidation;
using GameStore.Api.Contracts;

namespace GameStore.Api.Validators;

public class CreateGameDtoValidator : AbstractValidator<CreateGameDto>
{
    public CreateGameDtoValidator()
    {
        RuleFor(item => item.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(50);

        RuleFor(item => item.GenreId)
            .GreaterThan(0).WithMessage("GenreId must be greater than zero.");

        RuleFor(item => item.Price)
            .InclusiveBetween(0, 1000).WithMessage("Price must be between 0 and 1000.");

        RuleFor(item => item.ReleaseDate)
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Release date cannot be in the future.");
    }
}