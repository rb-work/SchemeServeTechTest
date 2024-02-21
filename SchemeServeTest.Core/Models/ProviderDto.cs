using System.Text.Json.Serialization;

namespace SchemeServeTest.Core.Models
{
    public class ProviderDto
    {
        [JsonPropertyName("providerId")]
        public string ProviderId { get; set; }

        [JsonPropertyName("locationIds")]
        public List<string> LocationIds { get; set; }

        [JsonPropertyName("organisationType")]
        public string OrganisationType { get; set; }

        [JsonPropertyName("ownershipType")]
        public string OwnershipType { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("brandId")]
        public string BrandId { get; set; }

        [JsonPropertyName("brandName")]
        public string BrandName { get; set; }

        [JsonPropertyName("registrationStatus")]
        public string RegistrationStatus { get; set; }

        [JsonPropertyName("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonPropertyName("companiesHouseNumber")]
        public string CompaniesHouseNumber { get; set; }

        [JsonPropertyName("charityNumber")]
        public string CharityNumber { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("postalAddressLine1")]
        public string PostalAddressLine1 { get; set; }

        [JsonPropertyName("postalAddressLine2")]
        public string PostalAddressLine2 { get; set; }

        [JsonPropertyName("postalAddressTownCity")]
        public string PostalAddressTownCity { get; set; }

        [JsonPropertyName("postalAddressCounty")]
        public string PostalAddressCounty { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("uprn")]
        public string Uprn { get; set; }

        [JsonPropertyName("onspdLatitude")]
        public double OnspdLatitude { get; set; }

        [JsonPropertyName("onspdLongitude")]
        public double OnspdLongitude { get; set; }

        [JsonPropertyName("mainPhoneNumber")]
        public string MainPhoneNumber { get; set; }

        [JsonPropertyName("inspectionDirectorate")]
        public string InspectionDirectorate { get; set; }

        [JsonPropertyName("constituency")]
        public string Constituency { get; set; }

        [JsonPropertyName("localAuthority")]
        public string LocalAuthority { get; set; }

        [JsonPropertyName("lastInspection")]
        public LastInspectionDto LastInspection { get; set; }
    }
}
