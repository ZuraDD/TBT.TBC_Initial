using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class PhotoUploadService : IPhotoUploadService
    {
        public IWebHostEnvironment Env { get; }

        private static string DefaultWebRootDirectoryName { get; set; } = "wwwroot";

        private string UploadFolderRelativePath = "Images/Person";

        private string WebRootPath { get; set; }

        public string UploadFolderAbsolutePath { get; set; }

        public PhotoUploadService(IWebHostEnvironment hostEnvironment)
        {
            Env = hostEnvironment;

            Init();
        }

        #region Public Methods

        public string GetWebRootPath()
        {
            return WebRootPath;
        }

        public async Task<string> UplodPhotoAndReturnRelativePath(IFormFile file, int personId, CancellationToken cancellationToken)
        {
            var fileName = GetUniqueFileName(file.FileName);

            var absoluteFilePath = GetAbsoluteUploadFilePathForPerson(personId, fileName);

            var relativeFilePath = GetRelativeUploadFilePathForPerson(personId, fileName);

            CleanDirectory(personId);

            await using (var fileStream = new FileStream(absoluteFilePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream, cancellationToken);
            }

            return relativeFilePath;
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            WebRootPath = !string.IsNullOrWhiteSpace(Env.WebRootPath) ? Env.WebRootPath : Path.Combine(Directory.GetCurrentDirectory(), DefaultWebRootDirectoryName);

            UploadFolderAbsolutePath = Path.Combine(WebRootPath, UploadFolderRelativePath);
        }

        private string GetAbsoluteUploadFolderPathForPerson(int personId)
        {
            return Path.Combine(UploadFolderAbsolutePath, $"{personId}");
        }

        private string GetAbsoluteUploadFilePathForPerson(int personId, string fileName)
        {
            return Path.Combine(UploadFolderAbsolutePath, $"{personId}", fileName);
        }

        private string GetRelativeUploadFilePathForPerson(int personId, string fileName)
        {
            return Path.Combine(UploadFolderRelativePath, $"{personId}", fileName);
        }

        private void CleanDirectory(int personId)
        {
            var directory = GetAbsoluteUploadFolderPathForPerson(personId);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                return;
            }

            var fileEntries = Directory.GetFiles(directory);

            foreach (var fileName in fileEntries)
                File.Delete(fileName);
        }

        private string GetUniqueFileName(string baseName)
        {
            return Guid.NewGuid() + "_" + baseName;
        }

        #endregion
    }
}
