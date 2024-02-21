using Microsoft.EntityFrameworkCore;
using SchemeServeTest.Core.Models;
using SchemeServeTest.Data;
using SchemeServeTest.Data.Models;

namespace SchemeServeTest.Core.Services
{
    public interface IDatabaseService
    {
        Task<ProviderDto> GetProviderAsync(string providerId);
        Task AddProviderAsync(ProviderDto providerDto);
    }

    public class DatabaseService : IDatabaseService
    {
        protected DataContext _dbContext { get; set; }
        public DatabaseService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProviderDto> GetProviderAsync(string providerId)
        {
            try
            {
                var provider = await _dbContext.Providers.AsNoTracking().Include(x => x.LastInspection).Include(x => x.Locations).FirstOrDefaultAsync(x => x.ProviderId == providerId && x.DateAdded >= DateTime.Now.AddMonths(-1));
                if (provider == null)
                {
                    return null;
                }
                else
                {
                    var providerDto = await GetProviderDtoAsync(provider);
                    return providerDto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving data: {ex.Message}", ex);
            }
        }

        public async Task AddProviderAsync(ProviderDto providerDto)
        {
            try
            {
                var provider = new Provider
                {
                    ProviderId = providerDto.ProviderId,
                    OrganisationType = providerDto.OrganisationType,
                    OwnershipType = providerDto.OwnershipType,
                    Type = providerDto.Type,
                    Name = providerDto.Name,
                    BrandId = providerDto.BrandId,
                    BrandName = providerDto.BrandName,
                    RegistrationStatus = providerDto.RegistrationStatus,
                    RegistrationDate = providerDto.RegistrationDate,
                    CompaniesHouseNumber = providerDto.CompaniesHouseNumber,
                    CharityNumber = providerDto.CharityNumber,
                    Website = providerDto.Website,
                    PostalAddressLine1 = providerDto.PostalAddressLine1,
                    PostalAddressLine2 = providerDto.PostalAddressLine2,
                    PostalAddressTownCity = providerDto.PostalAddressTownCity,
                    PostalAddressCounty = providerDto.PostalAddressCounty,
                    Region = providerDto.Region,
                    PostalCode = providerDto.PostalCode,
                    Uprn = providerDto.Uprn,
                    OnspdLatitude = providerDto.OnspdLatitude,
                    OnspdLongitude = providerDto.OnspdLongitude,
                    MainPhoneNumber = providerDto.MainPhoneNumber,
                    InspectionDirectorate = providerDto.InspectionDirectorate,
                    Constituency = providerDto.Constituency,
                    LocalAuthority = providerDto.LocalAuthority,
                    DateAdded = DateTime.UtcNow,
                };

                if (providerDto.LastInspection != null)
                {
                    provider.LastInspection = new LastInspection
                    {
                        Date = DateTime.Parse(providerDto.LastInspection.Date)
                    };
                }

                if (providerDto.LocationIds != null)
                {
                    provider.Locations = providerDto.LocationIds.Select(x => new Location
                    {
                        LocationId = x,
                    }).ToList();
                }

                await _dbContext.Providers.AddAsync(provider);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the database: {ex.Message}", ex);
            }
        }

        private async Task<ProviderDto> GetProviderDtoAsync(Provider provider)
        {
            try
            {
                return new ProviderDto
                {
                    ProviderId = provider.ProviderId,
                    OrganisationType = provider.OrganisationType,
                    OwnershipType = provider.OwnershipType,
                    Type = provider.Type,
                    Name = provider.Name,
                    BrandId = provider.BrandId,
                    BrandName = provider.BrandName,
                    RegistrationStatus = provider.RegistrationStatus,
                    RegistrationDate = provider.RegistrationDate,
                    CompaniesHouseNumber = provider.CompaniesHouseNumber,
                    CharityNumber = provider.CharityNumber,
                    Website = provider.Website,
                    PostalAddressLine1 = provider.PostalAddressLine1,
                    PostalAddressLine2 = provider.PostalAddressLine2,
                    PostalAddressTownCity = provider.PostalAddressTownCity,
                    PostalAddressCounty = provider.PostalAddressCounty,
                    Region = provider.Region,
                    PostalCode = provider.PostalCode,
                    Uprn = provider.Uprn,
                    OnspdLatitude = provider.OnspdLatitude,
                    OnspdLongitude = provider.OnspdLongitude,
                    MainPhoneNumber = provider.MainPhoneNumber,
                    InspectionDirectorate = provider.InspectionDirectorate,
                    Constituency = provider.Constituency,
                    LocalAuthority = provider.LocalAuthority,
                    LastInspection = provider.LastInspection != null ? new LastInspectionDto { Date = provider.LastInspection.Date.ToString("yyyy-MM-dd") } : null,
                    LocationIds = provider.Locations.Select(x => x.LocationId).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retriving provider details: {ex.Message}", ex);
            }
        }
    }
}
