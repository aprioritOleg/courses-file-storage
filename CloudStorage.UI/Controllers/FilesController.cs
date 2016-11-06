namespace CloudStorage.UI.Controllers
{
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using System.Configuration;
    using System.IO;
    using Models;
    /// <summary>
    /// Defines FilesController
    /// </summary>
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
            //List with subfolders which have to opened after adding files or folders
            ViewBag.ListSubfoldersID = new List<int>(); //treeview will be closed (folded)

            //return two model for treeview and for area, where will be displayed icons and filenames
             return View(new TreeViewAndBrowsingFilesModel() { TreeviewItems = _fileService.GetFilesByUserID(User.Identity.GetUserId()), IconItems = _fileService.GetFilesInFolderByUserID(0, User.Identity.GetUserId()) } );
        }
 
        //Returns user's files in specific folder 
        public PartialViewResult ShowUserFiles(int fileSystemStructureID)
        {
            return PartialView("_BrowsingFiles", _fileService.GetFilesInFolderByUserID(fileSystemStructureID, User.Identity.GetUserId()));
        }

        [HttpPost]
        public JsonResult Upload(int folderID)
        {
            int newFolderID = 0;
            //transfer uploaded files to Service
            foreach (string fileName in Request.Files)
            {
                newFolderID = _fileService.Create(new Domain.FileAggregate.FileInfo() {
                                                                        Name = Request.Files[fileName].FileName,
                                                                        CreationDate = DateTime.Now,
                                                                        Extension = System.IO.Path.GetExtension(Request.Files[fileName].FileName),
                                                                        OwnerId = User.Identity.GetUserId(),
                                                                        ParentID = folderID
                                                                         },
                                                                        Request.Files[fileName].InputStream, Server.MapPath(getPathToUserFolder()));
            }
            //List with subfolders which have to opened after adding files or folders
            ViewBag.ListSubfoldersID = _fileService.GetSubfoldersByFolderID(newFolderID);
            return GetModels(folderID);
        }
        //Folder will be added in table FileInfo
        [HttpPost]
        public JsonResult AddFolder(string folderName, int currentFolderID)
        {
          int newFolderId = _fileService.AddNewFolder(new Domain.FileAggregate.FileInfo() {
                                                                            Name = folderName,
                                                                            CreationDate = DateTime.Now,
                                                                            OwnerId = User.Identity.GetUserId(),
                                                                            ParentID = currentFolderID
                                                                       });

            ViewBag.ListSubfoldersID = _fileService.GetSubfoldersByFolderID(newFolderId);
            return GetModels(currentFolderID);
        }
        // Returns updated models for Treeview and for display area
        private JsonResult GetModels(int currentFolderID)
        {
            //create two model to return to partial views Treeview and BrowsingFiles
            var model = new TreeViewAndBrowsingFilesModel();
            model.TreeviewItems = _fileService.GetFilesByUserID(User.Identity.GetUserId());
            model.IconItems = _fileService.GetFilesInFolderByUserID(currentFolderID, User.Identity.GetUserId());

            var dataTreeview = RenderRazorViewToString(this.ControllerContext, "_Treeview", model.TreeviewItems);
            var dataArea = RenderRazorViewToString(this.ControllerContext, "_BrowsingFiles", model.IconItems);
            return Json(new { dataTreeview, dataArea });
        }
        // helper method to package up the partial view
        public static string RenderRazorViewToString(ControllerContext controllerContext,
             string viewName, object model)
        {
            controllerContext.Controller.ViewData.Model = model;

            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return stringWriter.GetStringBuilder().ToString();
            }
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