using FluentValidation;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core.FluentValidation;

public class AddVoucherRequestValidator : AbstractValidator<AddVoucherRequest>
{
    public AddVoucherRequestValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.ApplicationID).NotEmpty();
        RuleFor(x => x.TechTypeId).NotEmpty();
    }
}
