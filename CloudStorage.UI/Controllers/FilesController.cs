namespace CloudStorage.UI.Controllers
{
    using CloudStorage.Services.Interfaces;
    using CloudStorage.Domain.FileAggregate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Configuration;
    using CloudStorage.Services.Services;
    using CloudStorage.UI.Models;

    /// <summary>
    /// Defines FilesController
    /// </summary>
    public class FilesController : Controller
    {
        /// <summary>
        /// Holds FileService instance
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesController"/> class
        /// </summary>
        /// <param name="fileService">The tournament service</param>
        public FilesController(IFileService fileService)
        {
            this._fileService = fileService;
        }

        public ActionResult Index()
        {

            string s = ConfigurationManager.AppSettings["PathUserData"].ToString();
            FileInfo file = new FileInfo() { Name = "test", CreationDate = DateTime.Now, Extension = "txt" };

            this._fileService.Create(file);
            return View();
        }

        [HttpPost]
        public JsonResult Download(int fileId)
        {
            var file = this._fileService.GetFileById(fileId);

            if (file == null)
            {
                return Json("File does not exist.");
            }

            HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition",
                               "attachment; filename=" + file.Name + "." + file.Extension);
            string pathToFile = String.Format("~/{0}/{1}.dat", file.OwnerId, file.Id);
            response.TransmitFile(Server.MapPath(pathToFile));
            response.Flush();
            response.End();

            return Json("Success");
        }


        [HttpGet]
        public ActionResult Redact(FileInfo file)
        {

            IFileConverter converter = GetConverterInstance(file);
            RedactingViewModel redact = null;
            if (converter != null)
            {
                redact = new RedactingViewModel();
                redact.FilePath = GetFullFileName(file);
                redact.Extension = file.Extension;
                redact.Name = file.Name;
                redact.HtmlText = converter.ToHtml(redact.FilePath);
            }
            return View(redact);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Redact(RedactingViewModel file)
        {

            IFileConverter converter = GetConverterInstance(file.Extension);
           
            if (converter != null)
            {
                converter.FromHtml(file.FilePath, file.HtmlText);
            }

            return View(file);
        }


        // Summary:
        //     Represents model for passing data when Redact file 
        private string GetFullFileName(FileInfo file)
        {
            const string userDataPath = "PathUserData";
            return ConfigurationManager.AppSettings[userDataPath].ToString() + User.Identity.Name + file.Name + file.Extension;
        }
        private IFileConverter GetConverterInstance(FileInfo file)
        {
            switch (file.Extension)
            {
                case ".pdf":
                    return new PdfFileConverter();

                case ".docx":
                    return new DocxFileConverter();

                case ".txt":
                    return new TxtFileConverter();

                default:
                    break;
            }
            return null;
        }
        private IFileConverter GetConverterInstance(string extension)
        {
            switch (extension)
            {
                case ".pdf":
                    return new PdfFileConverter();

                case ".docx":
                    return new DocxFileConverter();

                case ".txt":
                    return new TxtFileConverter();

                default:
                    break;
            }
            return null;
        }
    }
}