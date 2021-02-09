using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Lookup.PolicyInsurer;
using InsuranceClaims.Services.Lookup.PolicyInsurer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsuranceClaims.API.Controllers
{
    [Authorize]
    public class PolicyInsurersController : BaseController
    {
        private readonly IPolicyInsurerService _policyInsurerService;

        public PolicyInsurersController(
            IPolicyInsurerService policyInsurerService,
            IResponseDTO response,
            IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _policyInsurerService = policyInsurerService;
        }


        [Route("api/policyinsurers")]
        [HttpGet]
        public IResponseDTO SearchPolicyInsurers([FromQuery] PolicyInsurerFilterDto filterDto)
        {
            _response = _policyInsurerService.SearchPolicyInsurers(filterDto);
            return _response;
        }

        [Route("api/policyinsurers/dropdown")]
        [HttpGet]
        public IResponseDTO GetPolicyInsurersDropdown()
        {
            _response = _policyInsurerService.GetPolicyInsurersDropdown();
            return _response;
        }


        [Route("api/policyinsurers")]
        [HttpPost]
        public async Task<IResponseDTO> CreatePolicyInsurer([FromBody] CreateUpdatePolicyInsurerDto options)
        {
            var validationResult = _policyInsurerService.ValidatePolicyInsurer(options);
            if (!validationResult.IsPassed)
            {
                return validationResult;
            }

            _response = await _policyInsurerService.CreatePolicyInsurer(options, LoggedInUserId);
            return _response;
        }


        [Route("api/policyinsurers/{id}")]
        [HttpPut]
        public async Task<IResponseDTO> UpdatePolicyInsurer([FromRoute] int id, [FromBody] CreateUpdatePolicyInsurerDto options)
        {
            var validationResult = _policyInsurerService.ValidatePolicyInsurer(options, id);
            if (!validationResult.IsPassed)
            {
                return validationResult;
            }

            _response = await _policyInsurerService.UpdatePolicyInsurer(id, options, LoggedInUserId);
            return _response;
        }


        [Route("api/policyinsurers/{id}")]
        [HttpDelete]
        public async Task<IResponseDTO> RemovePolicyInsurer([FromRoute] int id)
        {
            _response = await _policyInsurerService.RemovePolicyInsurer(id);
            return _response;
        }


        [Route("api/policyinsurers/{id}/activate")]
        [HttpPost]
        public async Task<IResponseDTO> Activate([FromRoute] int id)
        {
            _response = await _policyInsurerService.UpdateIsActive(id, true, LoggedInUserId);
            return _response;
        }

        [Route("api/policyinsurers/{id}/deactivate")]
        [HttpPost]
        public async Task<IResponseDTO> Deactivate([FromRoute] int id)
        {
            _response = await _policyInsurerService.UpdateIsActive(id, false, LoggedInUserId);
            return _response;
        }
    }
}
