﻿namespace CloudStorage.Entity.Interfaces
{
    using CloudStorage.Domain.FileAggregate;

    /// <summary>
    /// Defines specific contract for FileInfoRepository
    /// </summary>
    public interface IFileInfoRepository
    {
        int Add(FileInfo newFile);

        void Update(FileInfo updatedFile);

        void Remove(int id);

        FileInfo GetFileById(int id);

        FileInfo GetFileById_UserId(int fileId, string userId);
    }
}