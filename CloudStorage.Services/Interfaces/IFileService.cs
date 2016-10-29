namespace CloudStorage.Services.Interfaces
{
    using CloudStorage.Domain.FileAggregate;
    using System;

    /// <summary>
    /// Defines a contract for FileService.
    /// </summary>
    public interface IFileService
    {
        void Create(FileInfo file);

        void Edit(FileInfo file);

        FileInfo GetFileById(int fileId, string userId);

        void Delete(int id);    
    }
}
