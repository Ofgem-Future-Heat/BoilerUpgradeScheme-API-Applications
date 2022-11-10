using FluentValidation;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core.FluentValidation;

/// <summary>
/// Validator for the CreateApplicationRequest object
/// </summary>
public class CreateApplicationRequestValidator : AbstractValidator<CreateApplicationRequest>
{
    public CreateApplicationRequestValidator()
    {
        RuleFor(x => x).NotNull();

        RuleFor(x => x.BusinessAccountID).NotEmpty();
        
        // installation address
        RuleFor(x => x.InstallationAddress).NotNull();
        RuleFor(x => x.InstallationAddress.Line1).NotEmpty().MaximumLength(127);
        RuleFor(x => x.InstallationAddress.Line2).MaximumLength(127);
        RuleFor(x => x.InstallationAddress.Line3).MaximumLength(127);
        RuleFor(x => x.InstallationAddress.County).MaximumLength(31);
        RuleFor(x => x.InstallationAddress.Postcode).NotEmpty();
        When(x => x.InstallationAddressManualEntry == false, () =>
        {
            RuleFor(x => x.InstallationAddress.UPRN)
                .MaximumLength(12).WithMessage("UPRN must have 12 digits or fewer")
                .Matches("^[0-9]*$").WithMessage("UPRN can only contain numbers");
        });
    }
}
