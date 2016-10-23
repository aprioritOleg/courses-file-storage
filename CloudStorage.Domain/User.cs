namespace CloudStorage.Domain
{
    using CloudStorage.Domain.FileAggregate;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets a value indicating where information about files.
        /// </summary>
        /// <value>Information about files.</value>
        public virtual ICollection<FileInfo> FilesInfo { get; set; }
    }
}
