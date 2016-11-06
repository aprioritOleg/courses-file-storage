namespace CloudStorage.Services.Interfaces
{
    using CloudStorage.Domain.FileAggregate;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Defines a contract for FileService.
    /// </summary>
    public interface IFileService
    {
        int Create(Domain.FileAggregate.FileInfo file, Stream fileStream, string pathToUserFolder);

        int AddNewFolder(Domain.FileAggregate.FileInfo folder);

        void Edit(Domain.FileAggregate.FileInfo file);

        Domain.FileAggregate.FileInfo GetFileById(int fileId, string userId);

        void Delete(int id);

        List<Domain.FileAggregate.FileInfo> GetFilesByUserID(string userId);

        List<string> GetFilesInFolderByUserID(int currentFolder, string userId);

        List<int> GetSubfoldersByFolderID(int folderID);
    }
}
