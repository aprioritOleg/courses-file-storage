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

        FileInfo Get(int id);

        void Delete(int id);
    }
}
