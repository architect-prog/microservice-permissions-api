using FluentValidation;

namespace Microservice.Permissions.Core.Validators.Common;

public sealed class IdentifierArrayValidator : AbstractValidator<int[]>
{
    public IdentifierArrayValidator(IValidator<int> identifierValidator)
    {
        RuleForEach(x => x).SetValidator(identifierValidator);

        RuleFor(x => x)
            .Must(x => x.Distinct().Count() == x.Length)
            .WithMessage("'Identifiers' must be unique.");
    }
}