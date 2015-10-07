using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using CodeFirstStoredProcs;
using UserRolesAddedApp.Models.DAL.Input;
using UserRolesAddedApp.Models.DAL.Output;

namespace UserRolesAddedApp.Models.DAL
{
    public class UserRoleDal
    {
        private StoredProcContext db = new StoredProcContext();



        public void GetInsertAspNetUserRoles(UserRolesInput roles)
        {
            try
            {
                db.GetRegisterCompleteProc.CallStoredProc(roles);

            }
            catch (Exception e)
            {
                
               
            }
        }


        public List<User> GetUserRegisterCompleteResultSheet()
        {
            List<User> users = db.GetRegisterCompleteResultSheetProc.CallStoredProc().ToList<User>();

            return users;
        }


        public User GetIndividualUserInfo(int id)
        {
              IndividualUser_Input inputparams = new IndividualUser_Input() { Id = id };

            /* for multile object define the method with List and then returns the methods with list types*/
            /*for single object*/
            User user = db.GetIndividualUserInfo.CallStoredProc(inputparams).ToList<User>().FirstOrDefault();
            return user;
        }


        //public User GetUpdateUser(User model)
        //{
        //    User inputparams = new User()
        //    {
        //        Id = model.Id,
        //        UserName = model.UserName,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        Email=model.Email
        //    };
        //    // user.Id

        //    User user = db.GetUpdateAspNetUserProc.CallStoredProc(inputparams).ToList<User>().FirstOrDefault();

        //    return user;
        //}

    }
}