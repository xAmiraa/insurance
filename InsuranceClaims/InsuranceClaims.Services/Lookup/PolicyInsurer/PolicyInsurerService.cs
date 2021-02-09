using AutoMapper;
using InsuranceClaims.Core.Common;
using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.Data.DataContext;
using InsuranceClaims.DTO.Lookup.PolicyInsurer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Lookup.PolicyInsurer
{
    public class PolicyInsurerService : IPolicyInsurerService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IResponseDTO _response;
        private readonly IMapper _mapper;

        public PolicyInsurerService(AppDbContext appDbContext,
            IResponseDTO response,
            IMapper mapper)
        {
            _response = response;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IResponseDTO SearchPolicyInsurers(PolicyInsurerFilterDto filterDto)
        {
            try
            {
                var query = _appDbContext.PolicyInsurers.Where(x => !x.IsDeleted);

                if (filterDto != null)
                {
                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        query = query.Where(x => x.Name.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()));
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
               
                var datalist = _mapper.Map<List<PolicyInsurerDto>>(query.ToList());

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
        public IResponseDTO GetPolicyInsurersDropdown()
        {
            try
            {
                var query = _appDbContext.PolicyInsurers.Where(x => !x.IsDeleted && x.IsActive)
                            .Select(x => new Data.DbModels.LookupSchema.PolicyInsurer
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
        public async Task<IResponseDTO> CreatePolicyInsurer(CreateUpdatePolicyInsurerDto options, int userId)
        {
            try
            {
                var policyInsurer = new Data.DbModels.LookupSchema.PolicyInsurer
                {
                    Name = options.Name,
                    Description = options.Description,
                    IsActive = true
                };

                await _appDbContext.PolicyInsurers.AddAsync(policyInsurer);

                // save to the database
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Policy insurer is created successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> UpdatePolicyInsurer(int id, CreateUpdatePolicyInsurerDto options, int userId)
        {
            try
            {
                var policyInsurer = _appDbContext.PolicyInsurers.FirstOrDefault(x => x.Id == id);
                if (policyInsurer == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "Invalid id";
                    return _response;
                }

                policyInsurer.Name = options.Name;
                policyInsurer.Description = options.Description;

                _appDbContext.PolicyInsurers.Update(policyInsurer);

                // save to the database
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Policy insurer is updated successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> RemovePolicyInsurer(int id)
        {
            try
            {
                var policyInsurer = _appDbContext.PolicyInsurers.FirstOrDefault(x => x.Id == id);
                if (policyInsurer == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "Invalid id";
                    return _response;
                }

                policyInsurer.IsDeleted = true;

                _appDbContext.PolicyInsurers.Update(policyInsurer);

                // save to the database
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Policy insurer is removed successfully";
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
                var policyInsurer = _appDbContext.PolicyInsurers.FirstOrDefault(x => x.Id == id);
                if (policyInsurer == null)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("The specified policy insurer id does not exist.");
                    return _response;
                }
                else if (policyInsurer.IsDeleted)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified policy insurer '{policyInsurer.Name}' is deleted.");
                    return _response;
                }
                else if (policyInsurer.IsActive == isActive)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified policy insurer '{policyInsurer.Name}' is already updated.");
                    return _response;
                }

                policyInsurer.IsActive = isActive;
                policyInsurer.UpdatedBy = userId;
                policyInsurer.UpdatedOn = DateTime.Now;

                // save to the database
                _appDbContext.PolicyInsurers.Update(policyInsurer);
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Policy insurer is updated successfully";
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
        public IResponseDTO ValidatePolicyInsurer(CreateUpdatePolicyInsurerDto options, int id = 0)
        {
            try
            {
                if (_appDbContext.PolicyInsurers.Any(x => x.Id != id && !x.IsDeleted && x.Name.ToLower().Trim() == options.Name.ToLower().Trim()))
                {
                    _response.Errors.Add($"Name '{options.Name}' is already exist, please try a new one.'");
                }
            }
            catch (Exception ex)
            {
                _response.IsPassed = false;
                _response.Data = null;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            if (_response.Errors.Count > 0)
            {
                _response.IsPassed = false;
                _response.Data = null;
                return _response;
            }

            _response.IsPassed = true;
            return _response;
        }
    }
}
