using InsuranceClaims.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.UploadFiles
{
    public interface IUploadFilesService
    {
        Task<IResponseDTO> UploadFile(string path, IFormFile file, bool deleteOldFiles = false);
        Task<IResponseDTO> UploadFiles(string path, List<IFormFile> files, bool deleteOldFiles = false);
    }
}
