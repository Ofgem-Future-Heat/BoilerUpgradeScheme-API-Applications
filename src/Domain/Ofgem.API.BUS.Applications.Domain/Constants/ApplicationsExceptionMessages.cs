namespace Ofgem.API.BUS.Applications.Domain.Constants;

/// <summary>
/// This class defines exception messages related to the Applications Service.
/// </summary>
public static class ApplicationsExceptionMessages
{
    public const string NoGuidError = "GUID provided is null.";

    public const string NoBusinessAccount = "No business account provided.";

    public const string NoReferenceNumber = "No reference number provided.";

    public const string NoInstallationAddress = "No installation address provided";

    public const string NoInstallationAddressLine1 = "Installation Address Line 1 is required";

    public const string NoInstallationAddressPostcode = "Installation Address Postcode is required";

    public const string EmptyApplicationId = "Application ID cannot be an empty GUID";

    public const string NoSearchFilterPattern = "No search or filter pattern has been supplied.";

    public const string ApplicationWithIdNotFound = "An Application with the provided ID cannot be found.";

    public const string ConsentEmailRequestNotConfirmed = "The Owner Consent API returned a response indicating failure sending the consent email";

    public const string NoPropertyOwnerEmailAddress = "Property Owner Email Address is missing";

    public const string QuoteAmountMaxValue = "Quote amount must be under 1 million pounds";
}
