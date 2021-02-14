using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IPhotoUploadService
    {
        Task<string> UplodPhotoAndReturnRelativePath(IFormFile file, int personId, CancellationToken cancellationToken);

        string GetWebRootPath();
    }
}
