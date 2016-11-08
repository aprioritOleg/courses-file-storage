namespace CloudStorage.UI.Controllers
{
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using System.Configuration;
    using System.Web.UI.HtmlControls;
    using System.IO;

    /// <summary>
    /// Defines FilesController
    /// </summary>
    [Authorize]
    [RequireHttps]
    public class FilesController : Controller
    {
        /// <summary>
        /// Holds FileService instance
        /// </summary>
        private readonly IFileService _fileService;
        private const string PATH_USER_FOLDER = "PathUserData";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FilesController"/> class
        /// </summary>
        /// <param name="fileService">The tournament service</param>
        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }
       
        public ActionResult Index()
        {
            ViewBag.DataIconsFiles = _fileService.GetFilesInFolderByUserID(0, User.Identity.GetUserId());
            return View("Index", _fileService.GetFilesByUserID(User.Identity.GetUserId()));
        }
 
        //Returns user's files in specific folder 
        public ActionResult ShowUserFiles(int fileSystemStructureID)
        {
            ViewBag.DataIconsFiles = _fileService.GetFilesInFolderByUserID(fileSystemStructureID, User.Identity.GetUserId());
            return PartialView("PartialViewBrowsingFiles");
        }

        [HttpPost]
        public ActionResult Upload(int folderID)
        {
            //transfer uploaded files to Service
            foreach (string fileName in Request.Files)
            {
                _fileService.Create(new Domain.FileAggregate.FileInfo() {
                                                                        Name = Request.Files[fileName].FileName,
                                                                        CreationDate = DateTime.Now,
                                                                        Extension = System.IO.Path.GetExtension(Request.Files[fileName].FileName),
                                                                        OwnerId = User.Identity.GetUserId(),
                                                                        ParentID = folderID
                },
                                                                        Request.Files[fileName].InputStream, Server.MapPath(getPathToUserFolder()));
            }
            return PartialView("PartialViewBrowsingFiles");
        }
        //Folder will be added in table FileInfo
        [HttpPost]
        public PartialViewResult AddFolder(string folderName, int currentFolderID)
        {
          _fileService.AddNewFolder(new Domain.FileAggregate.FileInfo() {
                                                                            Name = folderName,
                                                                            CreationDate = DateTime.Now,
                                                                            OwnerId = User.Identity.GetUserId(),
                                                                            ParentID = currentFolderID
                                                                         });
            return PartialView("PartialViewTreeview", _fileService.GetFilesByUserID(User.Identity.GetUserId()));
        }
        //Returns the physical path to user folder on server
        private string getPathToUserFolder()
        {
            return Path.Combine(ConfigurationManager.AppSettings[PATH_USER_FOLDER].ToString(), User.Identity.GetUserId());
        }

        /// <summary>
        /// Download file.
        /// </summary>
        /// <param name="id">Identifier of file.</param>
        /// <returns>File for download.</returns>
        public ActionResult Download(int id)
        {
            var file = this._fileService.GetFileById(id, User.Identity.GetUserId());

            if (file == null)
            {
                return HttpNotFound();
            }

            return File(Url.Content(Server.MapPath(file.PathToFile))
                                    , GetContentType(file.Extension)
                                    , file.FullName);
        }

        /// <summary>
        /// Get type of content that depends on of the file extension.
        /// </summary>
        /// <param name="extension">Extension of file.</param>
        /// <returns>Content type.</returns>
        private string GetContentType(string extension)
        {
            switch (extension)
            {
                case "txt":
                    return "text/plain";
                case "jpeg":
                    return "image/pneg";
                case "jpg":
                    return "image/jpg";
                case "png":
                    return "image/png";
                case "pdf":
                    return "application/pdf";
                case ".flv":
                    return "video/x-flv";
                default:
                    return "application/unknown";
            }
        }
	}
}