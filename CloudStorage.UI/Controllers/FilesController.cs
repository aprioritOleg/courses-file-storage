﻿namespace CloudStorage.UI.Controllers
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
    using CloudStorage.Services.Services.ConverterServices.Factory;
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
            return View(_fileService.GetFilesByUserID(User.Identity.GetUserId()));
        }

        //Returns user's files in specific folder 
        public PartialViewResult ShowUserFiles(int fileSystemStructureID)
        {
            return PartialView("_BrowsingFiles", _fileService.GetFilesInFolderByUserID(fileSystemStructureID, User.Identity.GetUserId()));
        }

        [HttpPost]
        public PartialViewResult UploadFile(int currentFolderID)
        {
            //transfer uploaded files to Service
            foreach (string fileName in Request.Files)
            {
                _fileService.Create(new Domain.FileAggregate.FileInfo()
                {
                    Name = Request.Files[fileName].FileName,
                    CreationDate = DateTime.Now,
                    Extension = Path.GetExtension(Request.Files[fileName].FileName),
                    OwnerId = User.Identity.GetUserId(),
                    ParentID = currentFolderID
                },
                Request.Files[fileName].InputStream, Server.MapPath(getPathToUserFolder()));
            }

            return PartialView("_BrowsingFiles", _fileService.GetFilesInFolderByUserID(currentFolderID, User.Identity.GetUserId()));
        }
        //Folder will be added in table FileInfo
        [HttpPost]
        public PartialViewResult AddFolder(string folderName, int currentFolderID)
        {
            _fileService.AddNewFolder(new Domain.FileAggregate.FileInfo()
                                           {
                                               Name = folderName,
                                               CreationDate = DateTime.Now,
                                               OwnerId = User.Identity.GetUserId(),
                                               ParentID = currentFolderID
                                           });
            //returns partial view with model
            return PartialView("_BrowsingFiles", _fileService.GetFilesInFolderByUserID(currentFolderID, User.Identity.GetUserId()));
        }
        [HttpGet]
        public PartialViewResult UpdateTreeview(int currentFolderID)
        {
            //List with subfolders which have to opened after adding files or folders
            ViewBag.ListSubfoldersID = _fileService.GetSubfoldersByFolderID(currentFolderID);

            return PartialView("_Treeview", _fileService.GetFilesByUserID(User.Identity.GetUserId()));
        }
        //Returns the physical path to user folder on server
        private string getPathToUserFolder()
        {
            return Path.Combine(ConfigurationManager.AppSettings[PATH_USER_FOLDER].ToString(), User.Identity.GetUserId());
        }

        //Returns a thumbnail into view
        //The first parameter is a byte array that represents the file content
        //second parameter indicates the MIME content type.
        public ActionResult GetImage(int fileID)
        {
            return File(_fileService.GetImageBytes(fileID, Server.MapPath(getPathToUserFolder())), "image/png");
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


        // Summary
        //  Redact text file
        //
        // Parameters:
        //   id:
        // Identifier of redacting file
        [HttpGet]
        public JsonResult Redact(int id)
        {
            // Summary:
            //     Get the new instance of redacting FileInfo object
            var file = this._fileService.GetFileById(id, User.Identity.GetUserId());

            // Summary:
            //     If such file doesn't exist return error
            if (file == null)
            {
                return Json(new { error = true, reason = "File not found" }, JsonRequestBehavior.AllowGet);
            }
            IFileConverter converter = null;
            try
            {
                // Summary:
                //     Get the new instance of IFileConverter intarface that depend on file extension
                converter = FactoryConverter.CreateConveterInstace(file.Extension);

            }
            catch (Exception ex)
            {
                return Json(new { error = true, reason = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            // Summary:
            //     Create fileName that actual stored on server
            var name = file.Name.Substring(0, file.Name.IndexOf("."));
            name += ".dat";

            // Summary:
            //     htmlText string that represent all text from redacting file
            string htmlText = converter.ToHtml(Server.MapPath(getPathToUserFolder()) + "\\" + file.Id + ".dat");

            return Json(new { success = true, responseText = htmlText, fileName = file.Name }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Redact(int id, string htmlText)
        {
            // Summary:
            //     Get the new instance of redacting FileInfo object
            var file = this._fileService.GetFileById(id, User.Identity.GetUserId());

            IFileConverter converter;
            try
            {
                // Summary:
                //     Get the new instance of IFileConverter intarface that depend on file extension
                converter = FactoryConverter.CreateConveterInstace(file.Extension);

                // Summary:
                //     Create fileName that actual stored on server

                var name = file.Name.Substring(0, file.Name.IndexOf("."));
                name += ".dat";

                // Summary:
                //    Save all changed text to file
                converter.FromHtml(Server.MapPath(getPathToUserFolder()) + "\\" + file.Id + ".dat", htmlText);

            }
            catch (Exception ex)
            {                
                return Json(new { error = true, reason = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = htmlText, fileName = file.Name }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Domain.FileAggregate.FileInfo file = null;
            try
            {
                // Summary:
                //     Get the new instance of FileInfo object that will be deleted
                file = _fileService.GetFileById(id, User.Identity.GetUserId());

                // Summary:
                //    Delete file
                this._fileService.Delete(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
            // Summary:
            //     Return PartialView and using created instace of file for getting current folder
            return PartialView("_BrowsingFiles", _fileService.GetFilesInFolderByUserID(file.ParentID, User.Identity.GetUserId()));

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