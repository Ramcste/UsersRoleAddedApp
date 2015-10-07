using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using UserRolesAddedApp.Helpers;
using UserRolesAddedApp.Models;
using UserRolesAddedApp.Models.DAL.Input;

namespace UserRolesAddedApp.Areas.API
{
    public class TabController : ApiController
    {
        // GET: API/Tab

        private UserRolesAddedDBContext db = new UserRolesAddedDBContext();

        private UserRolesAddedDBForEntities dbForEntities=new UserRolesAddedDBForEntities();

        [System.Web.Mvc.Authorize(Roles = "Teacher,Student")]
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

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Authorize(Roles = "Teacher,Student")]
        public object DeleteUploadedFile([System.Web.Http.FromUri] int ImageId)
        {
            Image vi = dbForEntities.Images.Find(ImageId);
            dbForEntities.Images.Remove(vi);
            int hasDeleted = dbForEntities.SaveChanges();

            if (hasDeleted == 1)
            {
                try
                {
                    //remove uploaded files from file system
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    string filePath = vi.ImagePath + "/" + vi.ImageName;
                    System.IO.File.Delete(filePath);
                    System.IO.File.Delete(filePath.Replace("OriginalSize", "Thumbnail"));
                }
                catch (Exception)
                {
                    return Utilities.JsonOkObject(new { Message = "Error occurred while deleting image!" });
                }

            }

            return Utilities.JsonOkObject(new { Message = "Image Deleted Sucessfully!" });
        }

        public object SetPrimaryImage([System.Web.Http.FromUri] int ImageId, [System.Web.Http.FromUri] int UserId)
        {
            var ImagesList = dbForEntities.Images.Where(i => i.AddedByUserId == UserId).Select(i =>
                    new
                    {
                        imageId = i.ImageId
                    }
                ).ToList().AsEnumerable();

            foreach (var item in ImagesList)
            {
                Image vi = dbForEntities.Images.Find(item.imageId);
                vi.IsPrimary = item.imageId == ImageId ? true : false;
                dbForEntities.Entry(vi).State = EntityState.Modified;
            }

            int hasSaved = db.SaveChanges();

            return Utilities.JsonOkObject(new { Message = "Image is set primary!" });
        }

       
        public object SetLogoImage([System.Web.Http.FromUri] int ImageId, [System.Web.Http.FromUri] int UserId)
        {
            var ImagesList = dbForEntities.Images.Where(i => i.AddedByUserId == UserId).Select(i =>
                    new
                    {
                        imageId = i.ImageId
                    }
                ).ToList().AsEnumerable();

            foreach (var item in ImagesList)
            {
                Image vi = dbForEntities.Images.Find(item.imageId);
                vi.IsLogo = item.imageId == ImageId ? true : false;
                dbForEntities.Entry(vi).State = EntityState.Modified;
            }

            int hasSaved = db.SaveChanges();

            return Utilities.JsonOkObject(new { Message = "Image is set as Logo!" });
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
