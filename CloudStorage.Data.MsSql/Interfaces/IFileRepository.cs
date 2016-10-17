namespace CloudStorage.Data.MsSql.Interfaces
{
    using CloudStorage.Domain.FileAggregate;

    /// <summary>
    /// Defines specific contract for FileRepository
    /// </summary>
    public interface IFileRepository
    {
        void Add(File newFile);

        void Update(File updatedFile);

        void Remove(int id);
    }
}