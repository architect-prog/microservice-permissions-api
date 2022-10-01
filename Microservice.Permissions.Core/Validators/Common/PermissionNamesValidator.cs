using FluentValidation;

namespace Microservice.Permissions.Core.Validators.Common;

public sealed class PermissionNamesValidator : AbstractValidator<string[]>
{
    public PermissionNamesValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Permission names must have items.");

        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Permission names must have items.");

        RuleForEach(x => x)
            .NotNull()
            .WithMessage("'Name' must not be empty.");

        RuleForEach(x => x)
            .NotEmpty()
            .WithMessage("'Name' must not be empty.");

        RuleFor(x => x)
            .Must(x => x.Distinct().Count() == x.Length)
            .WithMessage("Permission names must be unique.");
    }
}