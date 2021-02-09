using AutoMapper;
using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.Data.DataContext;
using InsuranceClaims.DTO.Company.Company;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Company.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IResponseDTO _response;
        private readonly IMapper _mapper;

        public CompanyService(AppDbContext appDbContext,
            IResponseDTO response,
            IMapper mapper)
        {
            _response = response;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IResponseDTO SearchCompanies(CompanyFilterDto filterDto)
        {
            try
            {
                var query = _appDbContext.Companies
                                .Include(x => x.Country)
                                .Include(x => x.Creator)
                                .Include(x => x.Updator)
                                .Where(x => !x.IsDeleted);

                if (filterDto != null)
                {
                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        query = query.Where(x => x.Name.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Email))
                    {
                        query = query.Where(x => x.Email.Trim().ToLower().Contains(filterDto.Email.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.BusinessRegisterationNumber))
                    {
                        query = query.Where(x => x.BusinessRegisterationNumber.Trim().ToLower().Contains(filterDto.BusinessRegisterationNumber.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.TaxRegisterationNumber))
                    {
                        query = query.Where(x => x.TaxRegisterationNumber.Trim().ToLower().Contains(filterDto.TaxRegisterationNumber.Trim().ToLower()));
                    }
                    if (filterDto.CountryId != null)
                    {
                        query = query.Where(x => x.Country.Id == filterDto.CountryId);
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

                var datalist = _mapper.Map<List<CompanyDto>>(query.ToList());

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
        public async Task<IResponseDTO> CreateCompany(CreateCompanyDto options, int userId)
        {
            try
            {
                var company = _mapper.Map<Data.DbModels.CompanySchema.Company>(options);

                // Validate
                var validationResult = await ValidateCreatingCompany(options, company);
                if (!validationResult.IsPassed)
                {
                    return validationResult;
                }

                // Set the data
                company.CreatedBy = userId;
                company.CreatedOn = DateTime.Now;
                company.IsActive = true;

                // save to the database
                await _appDbContext.Companies.AddAsync(company);
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Company is created successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> RemoveCompany(int id, int userId)
        {
            try
            {
                var company = _appDbContext.Companies.FirstOrDefault(x => x.Id == id);
                if (company == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "Invalid id";
                    return _response;
                }
                else if (company.IsDeleted)
                {
                    _response.Errors.Add($"The specified company '{company.Name}' is already deleted.");
                }

                company.IsDeleted = true;
                company.UpdatedBy = userId;
                company.UpdatedOn = DateTime.Now;

                _appDbContext.Companies.Update(company);

                // save to the database
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Company is removed successfully";
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
        private async Task<IResponseDTO> ValidateCreatingCompany(CreateCompanyDto options, Data.DbModels.CompanySchema.Company company)
        {
            try
            {
                // Validate Name
                if (_appDbContext.Companies.Any(x => x.Name.Trim().ToLower() == options.Name.Trim().ToLower() && !x.IsDeleted))
                {
                    _response.Errors.Add($"Company name '{options.Name}' already exist, please try a new one.");
                }

                // Validate Email
                if (_appDbContext.Companies.Any(x => x.Email.Trim().ToLower() == options.Email.Trim().ToLower() && !x.IsDeleted))
                {
                    _response.Errors.Add($"Company email '{options.Email}' already exist, please try a new one.");
                }

                // Country
                var country = await _appDbContext.Countries.FirstOrDefaultAsync(x => x.Id == options.CountryId);
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
                    company.Country = country;
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
