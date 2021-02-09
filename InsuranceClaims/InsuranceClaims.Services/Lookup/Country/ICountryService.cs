using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Lookup.Country;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Lookup.Country
{
    public interface ICountryService
    {
        IResponseDTO SearchCountries(CountryFilterDto filterDto);
        IResponseDTO GetCountriesDropdown();
        Task<IResponseDTO> UpdateIsActive(int id, bool isActive, int userId);
    }
}
