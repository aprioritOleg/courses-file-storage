namespace CloudStorage.Services.Interfaces
{
    using CloudStorage.Domain.FileAggregate;
    using System;
    using System.Web;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for FileService.
    /// </summary>
    public interface IFileService
    {
        // void Create(FileInfo file);
        void Create(FileInfo file, HttpPostedFileBase httpFileBase, string pathToUserDirectory);

        void AddNewFolder(FileInfo folder);

        void Edit(FileInfo file);

        FileInfo GetFileById(int id);

        void Delete(int id);

       // List<FileInfo> GetDataFromSpecificFolder(int currentSystemID);
    }
}
