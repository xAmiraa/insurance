using AutoMapper;
using InsuranceClaims.Core.Common;
using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.Data.DataContext;
using InsuranceClaims.DTO.Lookup.IdentificationType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Lookup.IdentificationType
{
    public class IdentificationTypeService : IIdentificationTypeService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IResponseDTO _response;
        private readonly IMapper _mapper;

        public IdentificationTypeService(AppDbContext appDbContext,
            IResponseDTO response,
            IMapper mapper)
        {
            _response = response;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IResponseDTO SearchIdentificationTypes(IdentificationTypeFilterDto filterDto)
        {
            try
            {
                var query = _appDbContext.IdentificationTypes.Where(x => !x.IsDeleted);

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

                var datalist = _mapper.Map<List<IdentificationTypeDto>>(query.ToList());

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
        public IResponseDTO GetIdentificationTypesDropdown()
        {
            try
            {
                var query = _appDbContext.IdentificationTypes.Where(x => !x.IsDeleted && x.IsActive)
                            .Select(x => new Data.DbModels.LookupSchema.IdentificationType
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
        public async Task<IResponseDTO> CreateIdentificationType(CreateUpdateIdentificationTypeDto options, int userId)
        {
            try
            {
                var identificationType = new Data.DbModels.LookupSchema.IdentificationType
                {
                    Name = options.Name,
                    Description = options.Description,
                    IsActive = true
                };

                await _appDbContext.IdentificationTypes.AddAsync(identificationType);

                // save to the database
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Identification type is created successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> UpdateIdentificationType(int id, CreateUpdateIdentificationTypeDto options, int userId)
        {
            try
            {
                var identificationType = _appDbContext.IdentificationTypes.FirstOrDefault(x => x.Id == id);
                if (identificationType == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "Invalid id";
                    return _response;
                }

                identificationType.Name = options.Name;
                identificationType.Description = options.Description;

                _appDbContext.IdentificationTypes.Update(identificationType);

                // save to the database
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Identification type is updated successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> RemoveIdentificationType(int id)
        {
            try
            {
                var identificationType = _appDbContext.IdentificationTypes.FirstOrDefault(x => x.Id == id);
                if (identificationType == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "Invalid id";
                    return _response;
                }

                identificationType.IsDeleted = true;

                _appDbContext.IdentificationTypes.Update(identificationType);

                // save to the database
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Identification type is removed successfully";
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
                var identificationType = _appDbContext.IdentificationTypes.FirstOrDefault(x => x.Id == id);
                if (identificationType == null)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("The specified identification type id does not exist.");
                    return _response;
                }
                else if (identificationType.IsDeleted)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified identification type '{identificationType.Name}' is deleted.");
                    return _response;
                }
                else if (identificationType.IsActive == isActive)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified identification type '{identificationType.Name}' is already updated.");
                    return _response;
                }

                identificationType.IsActive = isActive;
                identificationType.UpdatedBy = userId;
                identificationType.UpdatedOn = DateTime.Now;

                // save to the database
                _appDbContext.IdentificationTypes.Update(identificationType);
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Identification type is updated successfully";
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
        public IResponseDTO ValidateIdentificationType(CreateUpdateIdentificationTypeDto options, int id = 0)
        {
            try
            {
                if (_appDbContext.IdentificationTypes.Any(x => x.Id != id && !x.IsDeleted && x.Name.ToLower().Trim() == options.Name.ToLower().Trim()))
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
