namespace CloudStorage.Entity.Repositories
{
    public abstract class ContextRepository
    {
        protected CloudStorageDbContext CreateContext()
        {
            return new CloudStorageDbContext();
        }
    }
}
