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
            FileInfo file = new FileInfo() { Name = "sfsef", CreationDate = DateTime.Now };

            this._fileService.Create(file);
            return View();
        }
	}
}