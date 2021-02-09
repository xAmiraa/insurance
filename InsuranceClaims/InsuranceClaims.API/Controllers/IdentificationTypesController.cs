using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Lookup.IdentificationType;
using InsuranceClaims.Services.Lookup.IdentificationType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsuranceClaims.API.Controllers
{
    [Authorize]
    public class IdentificationTypesController : BaseController
    {
        private readonly IIdentificationTypeService _identificationTypeService;

        public IdentificationTypesController(
            IIdentificationTypeService identificationTypeService,
            IResponseDTO response,
            IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _identificationTypeService = identificationTypeService;
        }


        [Route("api/identificationtypes")]
        [HttpGet]
        public IResponseDTO SearchIdentificationTypes([FromQuery] IdentificationTypeFilterDto filterDto)
        {
            _response = _identificationTypeService.SearchIdentificationTypes(filterDto);
            return _response;
        }

        [Route("api/identificationtypes/dropdown")]
        [HttpGet]
        public IResponseDTO GetIdentificationTypesDropdown()
        {
            _response = _identificationTypeService.GetIdentificationTypesDropdown();
            return _response;
        }


        [Route("api/identificationtypes")]
        [HttpPost]
        public async Task<IResponseDTO> CreateIdentificationType([FromBody] CreateUpdateIdentificationTypeDto options)
        {
            var validationResult = _identificationTypeService.ValidateIdentificationType(options);
            if (!validationResult.IsPassed)
            {
                return validationResult;
            }

            _response = await _identificationTypeService.CreateIdentificationType(options, LoggedInUserId);
            return _response;
        }


        [Route("api/identificationtypes/{id}")]
        [HttpPut]
        public async Task<IResponseDTO> UpdateIdentificationType([FromRoute] int id, [FromBody] CreateUpdateIdentificationTypeDto options)
        {
            var validationResult = _identificationTypeService.ValidateIdentificationType(options, id);
            if (!validationResult.IsPassed)
            {
                return validationResult;
            }

            _response = await _identificationTypeService.UpdateIdentificationType(id, options, LoggedInUserId);
            return _response;
        }


        [Route("api/identificationtypes/{id}")]
        [HttpDelete]
        public async Task<IResponseDTO> RemoveIdentificationType([FromRoute] int id)
        {
            _response = await _identificationTypeService.RemoveIdentificationType(id);
            return _response;
        }


        [Route("api/identificationtypes/{id}/activate")]
        [HttpPost]
        public async Task<IResponseDTO> Activate([FromRoute] int id)
        {
            _response = await _identificationTypeService.UpdateIsActive(id, true, LoggedInUserId);
            return _response;
        }

        [Route("api/identificationtypes/{id}/deactivate")]
        [HttpPost]
        public async Task<IResponseDTO> Deactivate([FromRoute] int id)
        {
            _response = await _identificationTypeService.UpdateIsActive(id, false, LoggedInUserId);
            return _response;
        }
    }
}
