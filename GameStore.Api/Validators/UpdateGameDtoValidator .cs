using FluentValidation;
using GameStore.Api.Contracts;

namespace GameStore.Api.Validators;

public class UpdateGameDtoValidator : AbstractValidator<UpdateGameDto>
{
    public UpdateGameDtoValidator()
    {
        RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required.")
           .MaximumLength(50);

        RuleFor(x => x.GenreId)
            .GreaterThan(0).WithMessage("GenreId must be greater than zero.");

        RuleFor(x => x.Price)
            .InclusiveBetween(0, 1000).WithMessage("Price must be between 0 and 1000.");

        RuleFor(x => x.ReleaseDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Release date cannot be in the future.");
    }
}