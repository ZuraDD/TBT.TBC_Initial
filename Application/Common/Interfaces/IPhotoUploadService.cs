namespace Application.Common.Interfaces
{
    public interface IPhotoUploadService
    {
        string GetAbsoluteUploadFolderPathForPerson(int personId);

        string GetAbsoluteUploadFilePathForPerson(int personId, string fileName);

        string GetRelativeUploadFilePathForPerson(int personId, string fileName);

        void CleanDirectory(int personId);

        string GetUniqueFileName(string baseName);
    }
}
