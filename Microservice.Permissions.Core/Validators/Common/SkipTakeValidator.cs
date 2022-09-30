using FluentValidation;

namespace Microservice.Permissions.Core.Validators.Common
{
    public sealed class SkipTakeValidator : AbstractValidator<(int? skip, int? take)>
    {
        public SkipTakeValidator()
        {
            RuleFor(x => x.skip)
                .GreaterThanOrEqualTo(0)
                .WithMessage("'Skip' must be greater than or equal to '0'.");

            RuleFor(x => x.take)
                .GreaterThanOrEqualTo(0)
                .WithMessage("'Take' must be greater than or equal to '0'.");
        }
    }
}