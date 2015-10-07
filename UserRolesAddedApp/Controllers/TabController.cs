using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageResizer;
using Microsoft.AspNet.Identity;
using UserRolesAddedApp.Models;
using UserRolesAddedApp.Models.DAL;
using UserRolesAddedApp.Models.DAL.Output;
using UserRolesAddedApp.Helpers;
using UserRolesAddedApp.Models.DAL.Input;

namespace UserRolesAddedApp.Controllers
{
    public class TabController : Controller
    {

        // GET: Tab

        private UserRolesAddedDBContext db = new UserRolesAddedDBContext();
        private UserRolesAddedDBForEntities dbForEntities=new UserRolesAddedDBForEntities();
        public ActionResult Index()
        {
            int userid = User.Identity.GetUserId<int>();
            User users = new UserRoleDal().GetIndividualUserInfo(userid);
            return View("Index", users);
        }


        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(User model)
        {
            StoredProcContext db = new StoredProcContext();
            User inputparams = new User
            {
                Id = model.Id,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
            db.GetUpdateAspNetUserProc.CallStoredProc(inputparams);

            //new UserRoleDal().GetUpdateUser(model);
            return RedirectToAction("Index",model);
        }

        [Authorize(Roles = "Student,Teacher")]
        public ActionResult SaveUploadedFile([System.Web.Http.FromUri] int UserId, [System.Web.Http.FromUri] string UserName)
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

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "OriginalSize\\"+UserId);
                    string pathString1 = System.IO.Path.Combine(originalDirectory.ToString(), "Thumbnail\\"+UserId);

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
                        vi.ImagePath = rootDirectory + "\\OriginalSize\\"+UserId ;
                        vi.IsPrimary = false;
                        vi.AddedDate = System.DateTime.UtcNow;
                        vi.AddedByUserId = Int32.Parse(User.Identity.GetUserId());
                        vi.FileSize = file.ContentLength;
                        vi.IsLogo = false;
                       
                        dbForEntities.Images.Add(vi);
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


        [Authorize(Roles = "Teacher,Student")]
        public object GetUploadedFile([System.Web.Http.FromUri] int UserId)
        {
            var uploadedFiles = dbForEntities.Images.Where(i => i.AddedByUserId == UserId).Select(i =>
                new
                {
                    imageId = i.ImageId,
                    imageName = i.ImageName,
                    imageSize = i.FileSize,
                    isPrimary = i.IsPrimary,
                    imagePath = i.ImagePath,
                    isLogo = i.IsLogo,
                    caption = i.Caption == null ? "" : i.Caption
                }
                ).ToList().AsEnumerable();

            return Utilities.JsonOkObject(new { UploadedFiles = uploadedFiles });
        }
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

    

    


