namespace CloudStorage.UI.Controllers
{
    using CloudStorage.Services.Interfaces;
    using CloudStorage.Domain.FileAggregate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using System.Configuration;
    using System.IO;
    using Models;
    using Entity;

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
        private List<TreeViewStructure> setDataInTreeview()
        {
            //This list will be returned to view Treeview.cshtml
            List<TreeViewStructure> treeViewList = new List<TreeViewStructure>();
            using (var context = new CloudStorageDbContext())
            {
                //Get all data from table FileSystemStructure
                var allFiles = context.FileSystemStructure.ToList();

                //Select files, which belong to current user
                foreach (var file in allFiles)
                {
                    Domain.FileAggregate.FileInfo fFile = context.Files.SingleOrDefault(file1 => file1.Id == file.FileID);
                    if (fFile.OwnerId == User.Identity.GetUserId())
                    {
                        TreeViewStructure element = new TreeViewStructure()
                        {
                            FileSystemStructureID = fFile.Id,
                            Name = fFile.Name,
                            ParentID = file.ParentID
                        };
                        //System.Diagnostics.Debug.WriteLine("FF " + fFile.Id + " " + fFile.Name + " " + file.ParentID);
                        treeViewList.Add(element);
                    }
                }
            }
            return treeViewList;
        }
        public ActionResult Index()
        {
            CreateUserFolder();
            ViewBag.DataIconsFiles = _fileService.getListFiles(getPathToUserFolder());
            return View("Index", setDataInTreeview());
        }
        public ActionResult ShowUserFiles(int fileSystemStructureID)
        {
            System.Diagnostics.Debug.WriteLine("fileSystemStructureID = " + fileSystemStructureID);
            return RedirectToAction("Index");
        }

        //Creation user folder after registration
        private void CreateUserFolder()
        {
            if (!System.IO.Directory.Exists(Server.MapPath(getPathToUserFolder())))
                System.IO.Directory.CreateDirectory(Server.MapPath(getPathToUserFolder()));
        }
        [HttpPost]
        public ActionResult Upload()
        {
            //transfer uploaded files to Service
            foreach (string fileName in Request.Files)
            {
                _fileService.Create(new Domain.FileAggregate.FileInfo() {
                                                                        Name = Request.Files[fileName].FileName,
                                                                        CreationDate = DateTime.Now,
                                                                        Extension = Path.GetExtension(Request.Files[fileName].FileName),
                                                                        OwnerId = User.Identity.GetUserId()
                                                                         },
                                                                        Request.Files[fileName], getPathToUserFolder());
            }
            ViewBag.DataIconsFiles = _fileService.getListFiles(getPathToUserFolder());
            //return PartialView("PartialViewBrowsingFiles", _fileService.getListFiles(getPathToUserFolder()));
            return PartialView("PartialViewBrowsingFiles");
        }
        //Returns the physical path to user folder on server
        private string getPathToUserFolder()
        {
            return ConfigurationManager.AppSettings["PathUserData"].ToString() + User.Identity.GetUserId() + "/";
        }
        public FilePathResult Download(string fileName)
        {
            //string path = Server.MapPath(pathToUserDirectory);
            return File(getPathToUserFolder() + fileName, "text/plain", fileName);
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