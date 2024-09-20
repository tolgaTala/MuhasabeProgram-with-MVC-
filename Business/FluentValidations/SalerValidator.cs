using Entities.Entities;
using FluentValidation;

namespace Business.FluentValidations
{
    public class SalerValidator : AbstractValidator<Saler>
    {
        public SalerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithName("Satıcı İsmi")
                ;

        }
    }
}
