using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Lookup.Currency;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Lookup.Currency
{
    public interface ICurrencyService
    {
        IResponseDTO SearchCurrencies(CurrencyFilterDto filterDto);
        IResponseDTO GetCurrenciesDropdown();
        Task<IResponseDTO> UpdateIsActive(int id, bool isActive, int userId);
    }
}
