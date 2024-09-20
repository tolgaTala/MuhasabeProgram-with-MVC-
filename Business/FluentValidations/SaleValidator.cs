using Entities.Entities;
using FluentValidation;

namespace Business.FluentValidations
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(x => x.Fiyat)
                .NotEmpty()
                ;
            RuleFor(x => x.Stok)
                .NotEmpty()
                ;
            RuleFor(x => x.SatisTarihi)
                .NotEmpty()
                ;

        }
    }
}
