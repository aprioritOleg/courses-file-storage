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
    using System.Web;
    using System.Collections.Generic;
    using System.IO;
    using System.Configuration;
    using Entity;

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
        public void Create(Domain.FileAggregate.FileInfo file, HttpPostedFileBase httpFileBase, string pathToUserDirectory)
        {
            //Adding information about file to database using FileInfoRepository
            //and return fileID of added file
            int fileID = _fileInfoRepository.Add(file);
            
            //Writing file on server
            if (httpFileBase != null && httpFileBase.ContentLength > 0)
            {
                var fileName = fileID + ".dat";
                var filePath = Path.Combine(HttpContext.Current.Server.MapPath(pathToUserDirectory), fileName);
       
                httpFileBase.SaveAs(filePath);
            }
        }

        public void AddNewFolder(Domain.FileAggregate.FileInfo folder)
        {
            _fileInfoRepository.Add(folder);
        }

       /* public List<Domain.FileAggregate.FileInfo> GetDataFromSpecificFolder(int currentSystemID)
        {
            //Request from tables 
            CloudStorageDbContext dbContext = new CloudStorageDbContext();

            var allFiles = dbContext.FileSystemStructure.ToList();

            using (var context = new CloudStorageDbContext())
            {

            }
        }*/
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
        public Domain.FileAggregate.FileInfo GetFileById(int id)
        {
            return this._fileInfoRepository.GetFileById(id);
        }

        public void Edit(Domain.FileAggregate.FileInfo file)
        {
            throw new NotImplementedException();
        }

        Domain.FileAggregate.FileInfo IFileService.GetFileById(int id)
        {
            throw new NotImplementedException();
        }
    }
}