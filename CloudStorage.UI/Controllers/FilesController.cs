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
        private List<Domain.FileAggregate.FileInfo> setDataInTreeview()
        {
            //This list will be returned to view Treeview.cshtml
            List<Domain.FileAggregate.FileInfo> treeViewList = new List<Domain.FileAggregate.FileInfo>();
            
            //ID logged in user
            string currentUserID = User.Identity.GetUserId();

            //Select all files logged in user
            using (var context = new CloudStorageDbContext())
            {
                treeViewList = context.Files.Where(u => u.OwnerId == currentUserID).ToList();
            }
            return treeViewList;
        }
        public ActionResult Index()
        {
            CreateUserFolder();
            Session["currentFolderID"] = 0; //0 - root folder
            ViewBag.DataIconsFiles = GetDataFromSpecificFolder(0);
            return View("Index", setDataInTreeview());
        }
        public List<string> GetDataFromSpecificFolder(int currentSystemID)
        {
            List<string> listFileNames = new List<string>();

            //ID logged in user
            string currentUserID = User.Identity.GetUserId();

            using (var context = new CloudStorageDbContext())
            {
                //Select files which belong to current user in specific folder
                var allFiles = context.Files.Where(u => u.ParentID == currentSystemID).Where(user => user.OwnerId == currentUserID);
                foreach (var file in allFiles)
                {
                     listFileNames.Add(file.Name);
                }
            }
            return listFileNames;
        }
        //Returns user's files in specific folder 
        public ActionResult ShowUserFiles(int fileSystemStructureID)
        {
            Session["currentFolderID"] = fileSystemStructureID;
            ViewBag.DataIconsFiles = GetDataFromSpecificFolder(fileSystemStructureID);
            return PartialView("PartialViewBrowsingFiles");
        }
        //Creation user folder after registration
        private void CreateUserFolder()
        {
            if (!Directory.Exists(Server.MapPath(getPathToUserFolder())))
                Directory.CreateDirectory(Server.MapPath(getPathToUserFolder()));
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
                                                                        OwnerId = User.Identity.GetUserId(),
                                                                        ParentID = (int)Session["currentFolderID"]
                                                                        },
                                                                        Request.Files[fileName], getPathToUserFolder());
            }
            return PartialView("PartialViewBrowsingFiles");
        }
        [HttpPost]
        public PartialViewResult AddFolder(string folderName)
        {
            _fileService.AddNewFolder(new Domain.FileAggregate.FileInfo() {
                                                                            Name = folderName,
                                                                            CreationDate = DateTime.Now,
                                                                            OwnerId = User.Identity.GetUserId(),
                                                                            ParentID = (int)Session["currentFolderID"]

                                                                        });
            return PartialView("PartialViewTreeview", setDataInTreeview());
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