using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudStorage.UI.Models
{
    //Returns data to the visual component Treeview
    public class TreeViewStructure
    {
        public int FileSystemStructureID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
    }
}