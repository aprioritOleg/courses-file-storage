﻿namespace CloudStorage.Data.MsSql.Interfaces
{
    using CloudStorage.Domain.FileAggregate;

    /// <summary>
    /// Defines specific contract for FileInfoRepository
    /// </summary>
    public interface IFileInfoRepository
    {
        void Add(FileInfo newFile);

        void Update(FileInfo updatedFile);

        void Remove(int id);
    }
}