using CloudStorage.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudStorage.Services.ConverterServices
{
    // <summary>
    // Defines an implementation of IFileConverter contract.
    public class TxtFileConverter : IFileConverter
    {
        // <summary>
        // Defines a method for converting file to HTML format.
        //
        // Parameters:
        //   pathToFile:
        // Path on server to editing file
        public string ToHtml(string pathToFile)
        {

            string documentText = string.Empty;
            using (StreamReader sr = new StreamReader(pathToFile, Encoding.UTF8))
            {
                documentText = sr.ReadToEnd();
            }
            //documentText = HttpUtility.HtmlEncode(documentText);
            documentText = documentText.Replace("\r\n", "\r");
            documentText = documentText.Replace("\n", "\r");
            documentText = documentText.Replace("\r", "<br>\r\n");
            documentText = documentText.Replace("  ", " &nbsp;");
            return documentText;
        }

        // <summary>
        // Defines a method for converting file to HTML format.
        //
        // Parameters:
        //   pathToFile:
        // Path on server to editing file
        //   htmlData:
        // Changed text of file in HTML format 
        public void FromHtml(string pathToFile, string htmlData)
        {

            //documentText = HttpUtility.HtmlDecode(documentText);
            htmlData = htmlData.Replace("\r\n", "\r");
            htmlData = htmlData.Replace("\n", "\r");
            htmlData = htmlData.Replace("<br>\r\n", "\r");
            htmlData = htmlData.Replace("&nbsp;", " ");
            string clearHtmlTags = @"(<[^>]+>)|\&\w+;";
            Regex regular = new Regex(clearHtmlTags);
            htmlData = regular.Replace(htmlData, "");
            using (StreamWriter writer = new StreamWriter(pathToFile))
            {
                writer.Write(htmlData);
            }

        }

    }
}
