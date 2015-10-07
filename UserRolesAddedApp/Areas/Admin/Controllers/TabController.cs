using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ImageResizer;
using Microsoft.AspNet.Identity;
using UserRolesAddedApp.Models;

namespace UserRolesAddedApp.Areas.Admin.Controllers
{
    public class TabController : Controller
    {
        // GET: Admin/Tab
        public ActionResult Upload()
        {
            return View();
        }

        private UserRolesAddedDBContext db=new UserRolesAddedDBContext();

        [Authorize(Roles = "Teacher,Student")]

        [HttpPost]

 /*
       public ActionResult SaveUploadedFile()
        {
            int insertedImageId = 0;

            bool isSavedSuccessfully = true;
            string fName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];

                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    string rootDirectory = string.Format("{0}\\Tab", Server.MapPath(@"~/Images"));
                    DirectoryInfo originalDirectory = new DirectoryInfo(rootDirectory);

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "OriginalSize\\");
                    string pathString1 = System.IO.Path.Combine(originalDirectory.ToString(), "Thumbnail\\");

                    string imageName = DateTime.UtcNow.Ticks.ToString() + fName.Substring(fName.LastIndexOf('.'));
                    bool isExists = System.IO.Directory.Exists(pathString);
                    bool isExistsInThumbnail = System.IO.Directory.Exists(pathString1);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    if (!isExistsInThumbnail)
                        System.IO.Directory.CreateDirectory(pathString1);

                    string path = string.Format("{0}\\{1}", pathString, imageName);

                    try
                    {

                        //Declare a new dictionary to store the parameters for the image versions.
                        var versions = new Dictionary<string, string>();

                        //Define the versions to generate
                        versions.Add("_Thumbnail",
                            "maxwidth=" + ConfigurationManager.AppSettings["Image.Thumbnail.Width"]);
                        versions.Add("_OriginalSize",
                            "maxwidth=" + ConfigurationManager.AppSettings["Image.Original.Width"]);

                        //Generate each version
                        foreach (var suffix in versions.Keys)
                        {
                            file.InputStream.Seek(0, SeekOrigin.Begin);
                            var localPath = suffix == "_OriginalSize" ? path : path.Replace("OriginalSize", "Thumbnail");
                            //Let the image builder add the correct extension based on the output file type
                            ImageBuilder.Current.Build(
                                new ImageJob(
                                    file.InputStream,
                                    localPath,
                                    new Instructions(versions[suffix]),
                                    false,
                                    false));
                        }

                        Image vi = new Image();
                        vi.ImageName = imageName;
                        vi.ImagePath = rootDirectory + "\\OriginalSize\\";
                        vi.AddedByUserId = Int32.Parse(User.Identity.GetUserId());
                        vi.IsPrimary = false;
                        vi.IsLogo = false;
                        vi.AddedDate = DateTime.UtcNow;
                        vi.FileSize = file.ContentLength;

                        db.Images.Add(vi);
                        isSavedSuccessfully = Convert.ToBoolean(db.SaveChanges());
                        insertedImageId = vi.ImageId;
                    }
                    catch (Exception ex)
                    {
                        isSavedSuccessfully = false;
                    }
                }

            }

            if (isSavedSuccessfully)
            {
                return Json(new { InsertedImageId = insertedImageId });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}