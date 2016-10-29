namespace CloudStorage.Domain.FileAggregate
{
    using System;
    using System.Collections.Generic;

    //Structure with folders and files
    public class FileSystemStructure
    {
        public int FileSystemStructureID { get; set; }
        public int FileID { get; set; }
        public int ParentID { get; set; }
    }
}
