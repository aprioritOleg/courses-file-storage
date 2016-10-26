namespace CloudStorage.Services.Services
{
    using CloudStorage.Domain.FileAggregate;
    using CloudStorage.Entity.Interfaces;
    using CloudStorage.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
        /// <param name="fileRepository">Instance of class which implements <see cref="IFileRepository"/>.</param>        
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
            _fileInfoRepository.Add(file);
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
        /// <param name="id">Identifier of file.</param>
        /// <returns>Information about file.</returns>
        public FileInfo GetFileById(int id)
        {
            return this._fileInfoRepository.GetFileById(id);
        }
    }
}