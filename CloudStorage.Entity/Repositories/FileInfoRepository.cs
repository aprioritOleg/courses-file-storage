namespace CloudStorage.Entity.Repositories
{
    using CloudStorage.Entity.Interfaces;
    using CloudStorage.Domain.FileAggregate;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines implementation of the IFileInfoRepository contract.
    /// </summary>
    public class FileInfoRepository : ContextRepository, IFileInfoRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoRepository"/> class.
        /// </summary>
        public FileInfoRepository()
        { }

        /// <summary>
        /// Adds new file.
        /// </summary>
        /// <param name="newFile">The file for adding.</param>
        public int Add(FileInfo newFile)
        {
            using (var context = CreateContext())
            {
                context.Files.Add(newFile);
                context.SaveChanges();
                return newFile.Id; 
            }
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
            using (var context = CreateContext())
            {
                var file = context.Files.Where(f => f.Id == id).SingleOrDefault();
                return (FileInfo)file;
            }
        }

        /// <summary>
        /// Get information about file by file id and user id.
        /// </summary>
        /// <param name="fileId">Identifier of file.</param>
        /// <param name="userId">Identifier of user.</param>
        /// <returns>Information about file.</returns>
        public FileInfo GetFileById_UserId(int fileId, string userId)
        {
            using (var context = CreateContext())
            {
                var file = context.Files
                                    .Where(f => f.Id == fileId && f.Users.Any(u => u.Id == userId))
                                    .SingleOrDefault();

                return (FileInfo)file;
            }
        }
    }
}
