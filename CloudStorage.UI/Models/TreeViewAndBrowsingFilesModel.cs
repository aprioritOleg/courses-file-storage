using System.Collections.Generic;

namespace CloudStorage.UI.Models
{
    public class TreeViewAndBrowsingFilesModel
    {
        public List<Domain.FileAggregate.FileInfo> TreeviewItems { get; set; }
        public List<string> IconItems { get; set; }
    }
}