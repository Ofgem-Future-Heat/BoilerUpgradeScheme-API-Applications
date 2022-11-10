using AutoMapper;
using Ofgem.API.BUS.Applications.Domain;
using Ofgem.API.BUS.Applications.Domain.Entities.CommsObjects;

namespace Ofgem.API.BUS.Applications.Core.AutoMapper.Converters;

/// <summary>
/// Converter to convert the create business accout request to the business account type
/// </summary>
public class ApplicationConverter : ITypeConverter<CreateApplicationRequest, Application>
{
    /// <summary>
    /// Converter
    /// </summary>
    /// <param name="source">Source type</param>
    /// <param name="destination">Destination Type</param>
    /// <param name="context"></param>
    /// <returns></returns>
    public Application Convert(CreateApplicationRequest source, Application destination, ResolutionContext context)
    {
        var application = new Application()
        {
            SubmitterId = source.InstallerUserAccountId,
            ApplicationDate = source.ApplicationDate,
            BusinessAccountId = source.BusinessAccountID,
            IsBeingAudited = source.IsBeingAudited,
            InstallationAddress = new InstallationAddress()
            {
                AddressLine1 = source.InstallationAddress.Line1,
                AddressLine2 = source.InstallationAddress.Line2,
                AddressLine3 = source.InstallationAddress.Line3,
                County = source.InstallationAddress.County,
                Postcode = source.InstallationAddress.Postcode,
                UPRN = source.InstallationAddress.UPRN,
                IsGasGrid = source.IsGasGrid,
                IsRural = source.RuralStatus,
                CountryCode = source.InstallationAddress.CountryCode
            },
            TechTypeId = source.TechTypeID,
            QuoteAmount = source.QuoteAmount,
            PreviousFuelType = source.PreviousFuelType,
            FuelTypeOther = source.FuelTypeOther,
            EpcExists = source.EpcExists,
            PropertyType = source.PropertyType,
            IsLoftCavityExempt = source.IsLoftCavityExempt,
            IsNewBuild = source.IsNewBuild,
            IsSelfBuild = source.IsSelfBuild,
            DateOfQuote = source.DateOfQuote,
            QuoteReferenceNumber = source.QuoteReference,
            QuoteProductPrice = source.TechnologyCost,
            CreatedBy = source.CreatedBy
        };


        if (source.EpcReferenceNumber != null)
        {
            application.Epc = new Epc()
            {
                EpcReferenceNumber = source.EpcReferenceNumber
            };
        }

        if (source.PropertyOwnerDetail != null && (
            source.PropertyOwnerDetail.FullName != null ||
            source.PropertyOwnerDetail.Email != null ||
            source.PropertyOwnerDetail.TelephoneNumber != null ||
            source.IsAssistedDigital != null ||
            source.IsWelshTranslation != null))
        {
            application.PropertyOwnerDetail = new PropertyOwnerDetail()
            {
                Email = source.PropertyOwnerDetail.Email,
                FullName = source.PropertyOwnerDetail.FullName,
                TelephoneNumber = source.PropertyOwnerDetail.TelephoneNumber,
                IsAssistedDigital = source.IsAssistedDigital,
                IsWelshTranslation = source.IsWelshTranslation
            };

            if (source.PropertyOwnerDetail.PropertyOwnerAddressUPRN != null ||
                source.PropertyOwnerDetail.PropertyOwnerAddressLine1 != null ||
                source.PropertyOwnerDetail.PropertyOwnerAddressLine2 != null ||
                source.PropertyOwnerDetail.PropertyOwnerAddressLine3 != null ||
                source.PropertyOwnerDetail.PropertyOwnerAddressPostcode != null ||
                source.PropertyOwnerDetail.PropertyOwnerAddressCounty != null ||
                source.PropertyOwnerDetail.PropertyOwnerAddressCountry != null)
            {
                application.PropertyOwnerDetail.PropertyOwnerAddress = new PropertyOwnerAddress()
                {
                    AddressLine1 = source.PropertyOwnerDetail.PropertyOwnerAddressLine1,
                    AddressLine2 = source.PropertyOwnerDetail.PropertyOwnerAddressLine2,
                    AddressLine3 = source.PropertyOwnerDetail.PropertyOwnerAddressLine3,
                    County = source.PropertyOwnerDetail.PropertyOwnerAddressCounty,
                    Postcode = source.PropertyOwnerDetail.PropertyOwnerAddressPostcode,
                    Country = source.PropertyOwnerDetail.PropertyOwnerAddressCountry,
                    UPRN = source.PropertyOwnerDetail.PropertyOwnerAddressUPRN,
                };
            }
        }

        return application;
    }
}
