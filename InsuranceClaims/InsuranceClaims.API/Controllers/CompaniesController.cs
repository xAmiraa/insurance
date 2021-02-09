using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Company.Company;
using InsuranceClaims.Services.Company.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsuranceClaims.API.Controllers
{
    [Authorize]
    public class CompaniesController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(
           ICompanyService companyService,
           IResponseDTO response,
           IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _companyService = companyService;
        }

        [Route("api/companies")]
        [HttpGet]
        public IResponseDTO SearchCompanies([FromQuery] CompanyFilterDto filterDto)
        {
            _response = _companyService.SearchCompanies(filterDto);
            return _response;
        }

        [Route("api/companies")]
        [HttpPost]
        public async Task<IResponseDTO> CreateCompany([FromBody] CreateCompanyDto options)
        {
            _response = await _companyService.CreateCompany(options, LoggedInUserId);
            return _response;
        }


        [Route("api/companies/{id}")]
        [HttpDelete]
        public async Task<IResponseDTO> RemoveCompany([FromRoute] int id)
        {
            _response = await _companyService.RemoveCompany(id, LoggedInUserId);
            return _response;
        }
    }
}
