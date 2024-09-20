using Entities.Entities;
using FluentValidation;

namespace Business.FluentValidations
{
    public class BuyValidator : AbstractValidator<Buy>
    {
        public BuyValidator()
        {
            RuleFor(x => x.Fiyat)
                .NotEmpty()
                ;
            RuleFor(x => x.Stok)
                .NotEmpty()
                ;
            RuleFor(x => x.AlisTarihi)
                .NotEmpty()
                ;

        }
    }
}
