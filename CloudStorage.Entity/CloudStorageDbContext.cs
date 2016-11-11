namespace CloudStorage.Entity
{
    using CloudStorage.Domain;
    using CloudStorage.Domain.FileAggregate;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class CloudStorageDbContext : IdentityDbContext<User>
    {
        public CloudStorageDbContext() : base("CloudStorageConnection")
        {
        }

        public CloudStorageDbContext(string connectionName) : base(connectionName) { }

        public DbSet<FileInfo> Files { get; set; }
    }
}