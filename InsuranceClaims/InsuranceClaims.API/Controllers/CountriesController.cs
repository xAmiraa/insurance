using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Lookup.Country;
using InsuranceClaims.Services.Lookup.Country;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsuranceClaims.API.Controllers
{
    [Authorize]
    public class CountriesController : BaseController
    {
        private readonly ICountryService _countryService;

        public CountriesController(
           ICountryService countryService,
           IResponseDTO response,
           IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _countryService = countryService;
        }

        [Route("api/countries")]
        [HttpGet]
        public IResponseDTO SearchCountries([FromQuery] CountryFilterDto filterDto)
        {
            _response = _countryService.SearchCountries(filterDto);
            return _response;
        }

        [Route("api/countries/dropdown")]
        [HttpGet]
        public IResponseDTO GetCountriesDropdown()
        {
            _response = _countryService.GetCountriesDropdown();
            return _response;
        }

        [Route("api/countries/{id}/activate")]
        [HttpPost]
        public async Task<IResponseDTO> Activate([FromRoute] int id)
        {
            _response = await _countryService.UpdateIsActive(id, true, LoggedInUserId);
            return _response;
        }

        [Route("api/countries/{id}/deactivate")]
        [HttpPost]
        public async Task<IResponseDTO> Deactivate([FromRoute] int id)
        {
            _response = await _countryService.UpdateIsActive(id, false, LoggedInUserId);
            return _response;
        }
    }
}
