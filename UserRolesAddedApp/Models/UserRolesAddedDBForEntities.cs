using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UserRolesAddedApp.Models
{
    public class UserRolesAddedDBForEntities:DbContext
    {
        static UserRolesAddedDBForEntities()
        {
            Database.SetInitializer<UserRolesAddedDBForEntities>(null);
        }

        public UserRolesAddedDBForEntities() : base("Name=UserRolesAddedEntities")
        {

        }

        public DbSet<Image> Images { get; set; }

    }
}