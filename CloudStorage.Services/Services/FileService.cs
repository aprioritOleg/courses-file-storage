namespace CloudStorage.Services.Services
{
    using CloudStorage.Data.MsSql.Interfaces;
    using CloudStorage.Domain.FileAggregate;
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
        private readonly IFileRepository _fileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="fileInfoRepository">Instance of class which implements <see cref="IFileInfoRepository"/>.</param>        
        /// <param name="fileRepository">Instance of class which implements <see cref="IFileRepository"/>.</param>        
        public FileService(IFileInfoRepository fileInfoRepository, IFileRepository fileRepository)
        {
            this._fileInfoRepository = fileInfoRepository;
            this._fileRepository = fileRepository;
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
        /// Gets file by its identifier.
        /// </summary>
        /// <param name="id">Identifier of file.</param>     
        public FileInfo Get(int id)
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
    }
}