namespace CloudStorage.UI.Controllers
{
    using CloudStorage.Services.Interfaces;
    using CloudStorage.Domain.FileAggregate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

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
	}
}