using Microsoft.AspNetCore.WebUtilities;

namespace WebApplicationProject.Interfaces
{
    public interface IStreamFileUploadService
    {
        Task<bool> UploadFile(MultipartReader reader, MultipartSection section);
    }
}
