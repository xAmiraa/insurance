using AutoMapper;
using InsuranceClaims.Core.Common;
using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.Data.DataContext;
using InsuranceClaims.DTO.Lookup.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Lookup.Country
{
    public class CountryService : ICountryService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IResponseDTO _response;
        private readonly IMapper _mapper;

        public CountryService(AppDbContext appDbContext,
            IResponseDTO response,
            IMapper mapper)
        {
            _response = response;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IResponseDTO SearchCountries(CountryFilterDto filterDto)
        {
            try
            {
                var query = _appDbContext.Countries.Where(x => !x.IsDeleted);

                if (filterDto != null)
                {
                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        query = query.Where(x => x.Name.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Code))
                    {
                        query = query.Where(x => x.Code.Trim().ToLower().Contains(filterDto.Code.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.NativeName))
                    {
                        query = query.Where(x => x.NativeName.Trim().ToLower().Contains(filterDto.NativeName.Trim().ToLower()));
                    }
                    if (filterDto.IsActive != null)
                    {
                        query = query.Where(x => x.IsActive == filterDto.IsActive);
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

                var datalist = _mapper.Map<List<CountryDto>>(query.ToList());

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
        public IResponseDTO GetCountriesDropdown()
        {
            try
            {
                var query = _appDbContext.Countries.Where(x => !x.IsDeleted && x.IsActive)
                            .Select(x => new Data.DbModels.LookupSchema.Country
                            {
                                Id = x.Id,
                                Name = x.Name
                            }).ToList();


                var datalist = _mapper.Map<List<LookupDto>>(query);

                _response.IsPassed = true;
                _response.Data = datalist;
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> UpdateIsActive(int id, bool isActive, int userId)
        {
            try
            {
                var country = _appDbContext.Countries.FirstOrDefault(x => x.Id == id);
                if (country == null)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("The specified country id does not exist.");
                    return _response;
                }
                else if(country.IsDeleted)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified country '{country.Name}' is deleted.");
                    return _response;
                }
                else if (country.IsActive == isActive)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified country '{country.Name}' is already updated.");
                    return _response;
                }

                country.IsActive = isActive;
                country.UpdatedBy = userId;
                country.UpdatedOn = DateTime.Now;

                // save to the database
                _appDbContext.Countries.Update(country);
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Country is updated successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
    }
}
