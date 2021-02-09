using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.DTO.Customer.Customer;
using InsuranceClaims.Services.Customer.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsuranceClaims.API.Controllers
{
    [Authorize]
    public class CustomersController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(
            ICustomerService customerService,
            IResponseDTO response,
            IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _customerService = customerService;
        }


        [Route("api/customers")]
        [HttpGet]
        public IResponseDTO SearchCustomers([FromQuery] CustomerFilterDto filterDto)
        {
            _response = _customerService.SearchCustomers(filterDto, LoggedInCompanyId);
            return _response;
        }


        [Route("api/customers")]
        [HttpPost]
        public async Task<IResponseDTO> CreateCustomer([FromBody] CreateCustomerDto options)
        {
            _response = await _customerService.CreateCustomer(options, LoggedInUserId, LoggedInCompanyId);
            return _response;
        }


        [Route("api/customers/{id}")]
        [HttpDelete]
        public async Task<IResponseDTO> RemoveCustomer([FromRoute] int id)
        {
            _response = await _customerService.RemoveCustomer(id, LoggedInUserId);
            return _response;
        }
    }
}
