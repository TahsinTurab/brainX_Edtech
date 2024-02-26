using Microsoft.AspNetCore.Http;

namespace brainX.Infrastructure.Services
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);
        public bool DeleteImage(string imageFileName);
    }
}