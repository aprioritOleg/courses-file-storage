namespace CloudStorage.Services.Interfaces
{
    using CloudStorage.Domain.FileAggregate;
    using System;

    /// <summary>
    /// Defines a contract for FileService.
    /// </summary>
    public interface IFileService
    {
        void Create(File file);

        void Edit(File file);

        File Get(int id);

        void Delete(int id);
    }
}
