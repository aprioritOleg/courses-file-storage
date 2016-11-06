using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudStorage.UI.Models
{
    public class RedactingViewModel
    {      
        public string FilePath { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public string HtmlText { get; set; }
    }
}