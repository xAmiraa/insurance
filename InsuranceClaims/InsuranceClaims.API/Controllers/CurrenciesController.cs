using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Lookup.Currency;
using InsuranceClaims.Services.Lookup.Currency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsuranceClaims.API.Controllers
{
    [Authorize]
    public class CurrenciesController : BaseController
    {
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(
           ICurrencyService currencyService,
           IResponseDTO response,
           IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _currencyService = currencyService;
        }

        [Route("api/currencies")]
        [HttpGet]
        public IResponseDTO SearchCurrencies([FromQuery] CurrencyFilterDto filterDto)
        {
            _response = _currencyService.SearchCurrencies(filterDto);
            return _response;
        }

        [Route("api/currencies/dropdown")]
        [HttpGet]
        public IResponseDTO GetCurrenciesDropdown()
        {
            _response = _currencyService.GetCurrenciesDropdown();
            return _response;
        }

        [Route("api/currencies/{id}/activate")]
        [HttpPost]
        public async Task<IResponseDTO> Activate([FromRoute] int id)
        {
            _response = await _currencyService.UpdateIsActive(id, true, LoggedInUserId);
            return _response;
        }

        [Route("api/currencies/{id}/deactivate")]
        [HttpPost]
        public async Task<IResponseDTO> Deactivate([FromRoute] int id)
        {
            _response = await _currencyService.UpdateIsActive(id, false, LoggedInUserId);
            return _response;
        }
    }
}
