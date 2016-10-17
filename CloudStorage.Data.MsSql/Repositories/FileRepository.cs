namespace CloudStorage.Data.MsSql.Repositories
{
    using CloudStorage.Data.MsSql.Interfaces;
    using CloudStorage.Domain.FileAggregate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines implementation of the IFileRepository contract.
    /// </summary>
    public class FileRepository : IFileRepository
    {
        private readonly CloudStorageDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepository"/> class.
        /// </summary>
        public FileRepository()
        {
            this._db = new CloudStorageDbContext();
        }

        /// <summary>
        /// Adds new file.
        /// </summary>
        /// <param name="newFile">The file for adding.</param>
        public void Add(File newFile)
        {
            this._db.Files.Add(newFile);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Updates specified file.
        /// </summary>
        /// <param name="updatedFi;le">Updated file.</param>
        public void Update(File updatedFile)
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
    }
}
