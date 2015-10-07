using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserRolesAddedApp.Models.DAL.Input;
using UserRolesAddedApp.Models.DAL.Output;

namespace UserRolesAddedApp.Models
{
    public partial class UserRolesAddedDBContext:DbContext
    {

        static UserRolesAddedDBContext()
        {
            Database.SetInitializer<UserRolesAddedDBContext>(null);
        }

        //public UserRolesAddedDBContext() : base("Name=UserRolesAddedEntities")
        //{

        //}

        public UserRolesAddedDBContext() : base("Name=DefaultConnection")
        {

        }

  

    }
}