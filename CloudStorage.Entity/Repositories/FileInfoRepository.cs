namespace CloudStorage.Entity.Repositories
{
    using CloudStorage.Entity.Interfaces;
    using CloudStorage.Domain.FileAggregate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines implementation of the IFileRepository contract.
    /// </summary>
    public class FileInfoRepository : IFileInfoRepository
    {
        private readonly CloudStorageDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoRepository"/> class.
        /// </summary>
        public FileInfoRepository()
        {
            this._db = new CloudStorageDbContext();
        }

        /// <summary>
        /// Adds new file.
        /// </summary>
        /// <param name="newFile">The file for adding.</param>
        public int Add(FileInfo newFile)
        {
            this._db.Files.Add(newFile);
            this._db.SaveChanges();
            
            return newFile.Id; 
        }
        /// <summary>
        /// Updates specified file.
        /// </summary>
        /// <param name="updatedFile">Updated file.</param>
        public void Update(FileInfo updatedFile)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes file by id.
        /// </summary>
        /// <param name="id">The id of file to remove.</param>
        public void Remove(int id)
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
            var file = this._db.Files.Where(f => f.Id == id).SingleOrDefault();
            return (FileInfo)file;
        }
    }
}
