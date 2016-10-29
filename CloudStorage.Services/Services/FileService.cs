namespace CloudStorage.Services.Services
{
    using CloudStorage.Domain;
    using CloudStorage.Domain.FileAggregate;
    using CloudStorage.Entity.Interfaces;
    using CloudStorage.Entity.Repositories;
    using CloudStorage.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Transactions;

    /// <summary>
    /// Defines an implementation of <see cref="IFileService"/> contract.
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IFileInfoRepository _fileInfoRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="fileInfoRepository">Instance of class which implements <see cref="IFileInfoRepository"/>.</param>
        public FileService(IFileInfoRepository fileInfoRepository)
        {
            this._fileInfoRepository = fileInfoRepository;
        }

        /// <summary>
        /// Creates a new file.
        /// </summary>
        /// <param name="file">File to create.</param>
        public void Create(FileInfo file)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Edits specified instance of file.
        /// </summary>
        /// <param name="fileToEdit">File to update.</param>
        public void Edit(FileInfo fileToEdit)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes file by its identifier.
        /// </summary>
        /// <param name="id">Identifier of file.</param>
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get information about file by id.
        /// </summary>
        /// <param name="fileId">Identifier of file.</param>
        /// <param name="userId">Identifier of user.</param>
        /// <returns>Information about file.</returns>
        public FileInfo GetFileById(int fileId, string userId)
        {
            FileInfo file = new FileInfo();
       
            using (var transaction = new TransactionScope())
            {
                file = this._fileInfoRepository.GetFileById_UserId(fileId, userId);
                transaction.Complete();
            }
            
            return file;
        }
    }
}