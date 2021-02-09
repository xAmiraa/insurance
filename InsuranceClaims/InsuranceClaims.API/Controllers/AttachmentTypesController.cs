using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Company.AttachmentType;
using InsuranceClaims.Services.Company.AttachmentType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsuranceClaims.API.Controllers
{
    [Authorize]
    public class AttachmentTypesController : BaseController
    {
        private readonly IAttachmentTypeService _attachmentTypeService;

        public AttachmentTypesController(
            IAttachmentTypeService attachmentTypeService,
            IResponseDTO response,
            IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _attachmentTypeService = attachmentTypeService;
        }


        [Route("api/attachmenttypes")]
        [HttpGet]
        public IResponseDTO SearchAttachmentTypes([FromQuery] AttachmentTypeFilterDto filterDto)
        {
            _response = _attachmentTypeService.SearchAttachmentTypes(filterDto, LoggedInCompanyId);
            return _response;
        }

        [Route("api/attachmenttypes/dropdown")]
        [HttpGet]
        public IResponseDTO GetAttachmentTypesDropdown()
        {
            _response = _attachmentTypeService.GetAttachmentTypesDropdown(LoggedInCompanyId);
            return _response;
        }


        [Route("api/attachmenttypes")]
        [HttpPost]
        public async Task<IResponseDTO> CreateAttachmentType([FromBody] CreateUpdateAttachmentTypeDto options)
        {
            _response = await _attachmentTypeService.CreateAttachmentType(options, LoggedInUserId, LoggedInCompanyId);
            return _response;
        }


        [Route("api/attachmenttypes/{id}")]
        [HttpPut]
        public async Task<IResponseDTO> UpdateAttachmentType([FromRoute] int id, [FromBody] CreateUpdateAttachmentTypeDto options)
        {
            _response = await _attachmentTypeService.UpdateAttachmentType(id, options, LoggedInUserId, LoggedInCompanyId);
            return _response;
        }


        [Route("api/attachmenttypes/{id}")]
        [HttpDelete]
        public async Task<IResponseDTO> RemoveAttachmentType([FromRoute] int id)
        {
            _response = await _attachmentTypeService.RemoveAttachmentType(id, LoggedInUserId);
            return _response;
        }


        [Route("api/attachmenttypes/{id}/activate")]
        [HttpPost]
        public async Task<IResponseDTO> Activate([FromRoute] int id)
        {
            _response = await _attachmentTypeService.UpdateIsActive(id, true, LoggedInUserId);
            return _response;
        }

        [Route("api/attachmenttypes/{id}/deactivate")]
        [HttpPost]
        public async Task<IResponseDTO> Deactivate([FromRoute] int id)
        {
            _response = await _attachmentTypeService.UpdateIsActive(id, false, LoggedInUserId);
            return _response;
        }
    }
}
