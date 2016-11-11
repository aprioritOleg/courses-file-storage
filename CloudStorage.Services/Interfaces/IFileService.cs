﻿namespace CloudStorage.Services.Interfaces
{
    using CloudStorage.Domain.FileAggregate;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Defines a contract for FileService.
    /// </summary>
    public interface IFileService
    {
        void Create(Domain.FileAggregate.FileInfo file, Stream fileStream, string pathToUserFolder);

        void AddNewFolder(Domain.FileAggregate.FileInfo folder);

        void Edit(Domain.FileAggregate.FileInfo file);

        Domain.FileAggregate.FileInfo GetFileById(int fileId, string userId);

        void Delete(int id);

        List<Domain.FileAggregate.FileInfo> GetFilesByUserID(string userId);

        List<Domain.FileAggregate.FileInfo> GetFilesInFolderByUserID(int currentFolder, string userId);

        List<int> GetSubfoldersByFolderID(int folderID);

        byte[] GetImageBytes(int fileID, string pathToUserFolder);
    }
}
