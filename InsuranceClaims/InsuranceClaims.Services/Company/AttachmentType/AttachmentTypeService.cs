using AutoMapper;
using InsuranceClaims.Core.Common;
using InsuranceClaims.Core.Interfaces;
using InsuranceClaims.Data.DataContext;
using InsuranceClaims.DTO.Company.AttachmentType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.Company.AttachmentType
{
    public class AttachmentTypeService : IAttachmentTypeService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IResponseDTO _response;
        private readonly IMapper _mapper;

        public AttachmentTypeService(AppDbContext appDbContext,
            IResponseDTO response,
            IMapper mapper)
        {
            _response = response;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IResponseDTO SearchAttachmentTypes(AttachmentTypeFilterDto filterDto, int companyId)
        {
            try
            {
                var query = _appDbContext.CompanyAttachmentTypes.Where(x => !x.IsDeleted);

                // Filter by logged in company
                if (companyId > 0)
                {
                    query = query.Where(x => x.Company.Id == companyId);
                }

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

                var datalist = _mapper.Map<List<AttachmentTypeDto>>(query.ToList());

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
        public IResponseDTO GetAttachmentTypesDropdown(int companyId)
        {
            try
            {
                var query = _appDbContext.CompanyAttachmentTypes.Where(x => !x.IsDeleted && x.IsActive && x.Company.Id == companyId)
                            .Select(x => new Data.DbModels.CompanySchema.CompanyAttachmentType
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
        public async Task<IResponseDTO> CreateAttachmentType(CreateUpdateAttachmentTypeDto options, int userId, int companyId)
        {
            try
            {
                var attachmentType = _mapper.Map<Data.DbModels.CompanySchema.CompanyAttachmentType>(options);

                // Validate
                var validationResult = await ValidateAttachmentType(options, attachmentType, companyId);
                if (!validationResult.IsPassed)
                {
                    return validationResult;
                }

                // Set the data
                attachmentType.CreatedBy = userId;
                attachmentType.CreatedOn = DateTime.Now;
                attachmentType.IsActive = true;

                // save to the database
                await _appDbContext.CompanyAttachmentTypes.AddAsync(attachmentType);
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Attachment type is created successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> UpdateAttachmentType(int id, CreateUpdateAttachmentTypeDto options, int userId, int companyId)
        {
            try
            {
                // Validate Id
                var attachmentType = await _appDbContext.CompanyAttachmentTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (attachmentType == null)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified attachment type id does not exist.");
                    return _response;
                }
                else if (attachmentType.IsDeleted)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified attachment type '{attachmentType.Name}' is deleted.");
                    return _response;
                }
                else
                {
                    attachmentType.UpdatedBy = userId;
                    attachmentType.UpdatedOn = DateTime.Now;
                    attachmentType.Name = options.Name;
                }

                // Validate
                var validationResult = await ValidateAttachmentType(options, attachmentType, companyId, id);
                if (!validationResult.IsPassed)
                {
                    return validationResult;
                }

                // save to the database
                _appDbContext.CompanyAttachmentTypes.Update(attachmentType);
                var save = await _appDbContext.SaveChangesAsync();
                if (save == 0)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("Database did not save the object");
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Attachment type is updated successfully";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }

            return _response;
        }
        public async Task<IResponseDTO> RemoveAttachmentType(int id, int userId)
        {
            try
            {
                var attachmentType = _appDbContext.CompanyAttachmentTypes.FirstOrDefault(x => x.Id == id);
                if (attachmentType == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "Invalid id";
                    return _response;
                }

                attachmentType.IsDeleted = true;

                _appDbContext.CompanyAttachmentTypes.Update(attachmentType);

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
                var attachmentType = _appDbContext.CompanyAttachmentTypes.FirstOrDefault(x => x.Id == id);
                if (attachmentType == null)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add("The specified policy insurer id does not exist.");
                    return _response;
                }
                else if (attachmentType.IsDeleted)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified policy insurer '{attachmentType.Name}' is deleted.");
                    return _response;
                }
                else if (attachmentType.IsActive == isActive)
                {
                    _response.IsPassed = false;
                    _response.Errors.Add($"The specified policy insurer '{attachmentType.Name}' is already updated.");
                    return _response;
                }

                attachmentType.IsActive = isActive;
                attachmentType.UpdatedBy = userId;
                attachmentType.UpdatedOn = DateTime.Now;

                // save to the database
                _appDbContext.CompanyAttachmentTypes.Update(attachmentType);
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
        private async Task<IResponseDTO> ValidateAttachmentType(CreateUpdateAttachmentTypeDto options, Data.DbModels.CompanySchema.CompanyAttachmentType attachmentType, int companyId, int id = 0)
        {
            try
            {
                // Validate Name
                if (_appDbContext.CompanyAttachmentTypes.Any(x => x.Name.Trim().ToLower() == options.Name.Trim().ToLower() && !x.IsDeleted && x.Company.Id == companyId && x.Id != id))
                {
                    _response.Errors.Add($"Attachment type name '{options.Name}' already exist, please try a new one.");
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
                    attachmentType.Company = company;
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
