using FluentValidation;

namespace Microservice.Permissions.Core.Validators.Common;

public sealed class IdentifierValidator : AbstractValidator<int>
{
    public IdentifierValidator()
    {
        RuleFor(x => x)
            .GreaterThan(0)
            .WithMessage("'Identifier' must be greater than '0'.");;
    }
}