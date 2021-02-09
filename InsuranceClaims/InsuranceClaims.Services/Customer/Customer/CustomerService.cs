using AutoMapper;
using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.Data.DataContext;
using InsuranceClaims.DTO.Customer.Customer;
using InsuranceClaims.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Customer.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IResponseDTO _response;
        private readonly IMapper _mapper;

        public CustomerService(AppDbContext appDbContext,
            IResponseDTO response,
            IMapper mapper)
        {
            _response = response;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IResponseDTO SearchCustomers(CustomerFilterDto filterDto, int companyId)
        {
            try
            {
                var query = _appDbContext.Customers
                                .Where(x => !x.IsDeleted);

                // Filter by logged in company
                if (companyId > 0)
                {
                    query = query.Where(x => x.Company.Id == companyId);
                }

                if (filterDto != null)
                {
                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        query = query.Where(x => x.BusinessName.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()));
                    }
                }

                //Check Sort Property
                if (!string.IsNullOrEmpty(filterDto?.SortProperty))
                {
                    //query = query.OrderBy(
                    //    string.Format("{0} {1}", filterDto.SortProperty, filterDto.IsAscending ? "ASC" : "DESC"));
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                // Pagination
                var total = query.Count();
                if (filterDto.PageIndex.HasValue && filterDto.PageSize.HasValue)
                {
                    query = query.Skip((filterDto.PageIndex.Value - 1) * filterDto.PageSize.Value).Take(filterDto.PageSize.Value);
                }

                var datalist = _mapper.Map<List<CustomerDto>>(query.ToList());

                _response.IsPassed = true;
                _response.Data = new
                {
                    List = datalist,
                    Total = total,
                };
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> CreateCustomer(CreateCustomerDto options, int userId, int companyId)
        {
            try
            {
                var customer = _mapper.Map<Data.DbModels.CustomerSchema.Customer>(options);

                // Validate
                var validationResult = await ValidateCreatingCustomer(options, customer, companyId);
                if (!validationResult.IsPassed)
                {
                    return validationResult;
                }

                // Set the data
                customer.CreatedBy = userId;
                customer.CreatedOn = DateTime.Now;
                customer.IsActive = true;
                customer.AccountManager.CreatedBy = userId;
                customer.AccountManager.CreatedOn = DateTime.Now;
                customer.CustomerContacts.Select(x => { x.CreatedBy = userId; x.CreatedOn = DateTime.Now; return x; }).ToList();
                customer.CustomerBeneficiaries.Select(x => { x.CreatedBy = userId; x.CreatedOn = DateTime.Now; return x; }).ToList();

                // save to the database
                await _appDbContext.Customers.AddAsync(customer);
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Customer is created successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> RemoveCustomer(int id, int userId)
        {
            try
            {
                var customer = _appDbContext.Customers.FirstOrDefault(x => x.Id == id);
                if (customer == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "Invalid id";
                    return _response;
                }

                customer.IsDeleted = true;
                customer.UpdatedBy = userId;
                customer.UpdatedOn = DateTime.Now;

                _appDbContext.Customers.Update(customer);

                // save to the database
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Customer is removed successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        
        // Validate
        private async Task<IResponseDTO> ValidateCreatingCustomer(CreateCustomerDto options, Data.DbModels.CustomerSchema.Customer customer, int companyId)
        {
            try
            {
                // Validate Code
                if (_appDbContext.Customers.Any(x => x.Code.Trim().ToLower() == options.Code.Trim().ToLower() && !x.IsDeleted && x.Company.Id == companyId))
                {
                    _response.Errors.Add($"Customer code '{options.Code}' already exist, please try a new one.");
                }

                // Validate Employer
                if (_appDbContext.CompanyEmployers.Any(x => x.Name.Trim().ToLower() == options.Employer.Name.Trim().ToLower() && !x.IsDeleted && x.Company.Id == companyId))
                {
                    _response.Errors.Add($"Employer name '{options.Employer.Name}' already exist, please try a new one.");
                }

                // Company
                var company = await _appDbContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
                if (company == null)
                {
                    _response.Errors.Add($"The specified company id does not exist.");
                }
                else if (company.IsDeleted)
                {
                    _response.Errors.Add($"The specified company '{company.Name}' is deleted.");
                }
                else
                {
                    customer.Company = company;
                }

                // Manager
                var manager = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == options.AccountManagerId);
                if (manager == null)
                {
                    _response.Errors.Add($"The specified manager id does not exist.");
                }
                else if (manager.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Errors.Add($"The specified manager '{manager.FirstName} {manager.LastName}' is not active.");
                }
                else
                {
                    customer.AccountManager = manager;
                }

                foreach(var contact in customer.CustomerContacts)
                {
                    // Country
                    var country = await _appDbContext.Countries.FirstOrDefaultAsync(x => x.Id == contact.Country.Id);
                    if (country == null)
                    {
                        _response.Errors.Add($"The specified country id does not exist.");
                    }
                    else if (country.IsDeleted)
                    {
                        _response.Errors.Add($"The specified country '{country.Name}' is deleted.");
                    }
                    else
                    {
                        contact.Country = country;
                    }
                }

                _response.IsPassed = true;
            }
            catch (Exception ex)
            {
                _response.IsPassed = false;
                _response.Data = null;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            if (_response.Errors.Count > 0)
            {
                _response.Errors = _response.Errors.Distinct().ToList();
                _response.IsPassed = false;
                _response.Data = null;
                return _response;
            }

            return _response;
        }
    }
}
