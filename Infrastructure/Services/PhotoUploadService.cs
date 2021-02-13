using System;
using System.IO;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services
{
    public class PhotoUploadService : IPhotoUploadService
    {
        public IWebHostEnvironment Env { get; }

        public string DefaultWebRootDirectoryName { get; set; } = "wwwroot";

        public string UploadFolderRelativePath = "Images/Person";

        public string WebRootPath { get; }

        public string UploadFolderAbsolutePath { get; set; }

        public PhotoUploadService(IWebHostEnvironment hostEnvironment)
        {
            Env = hostEnvironment;

            WebRootPath = GetWebRootPath();

            UploadFolderAbsolutePath = Path.Combine(WebRootPath, UploadFolderRelativePath);
        }

        private string GetWebRootPath()
        {
            return !string.IsNullOrWhiteSpace(Env.WebRootPath) ? Env.WebRootPath : Path.Combine(Directory.GetCurrentDirectory(), DefaultWebRootDirectoryName);
        }

        public string GetAbsoluteUploadFolderPathForPerson(int personId)
        {
            return Path.Combine(UploadFolderAbsolutePath, $"{personId}");
        }

        public string GetAbsoluteUploadFilePathForPerson(int personId, string fileName)
        {
            return Path.Combine(UploadFolderAbsolutePath, $"{personId}", fileName);
        }

        public string GetRelativeUploadFilePathForPerson(int personId, string fileName)
        {
            return Path.Combine(UploadFolderRelativePath, $"{personId}", fileName);
        }

        public void CleanDirectory(int personId)
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

        public string GetUniqueFileName(string baseName)
        {
            return Guid.NewGuid() + "_" + baseName;
        }
    }
}
