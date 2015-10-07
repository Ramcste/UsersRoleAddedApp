using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstStoredProcs;

namespace UserRolesAddedApp.Models.DAL.Output
{
    public class User
    {
        [StoredProcAttributes.Name("Id")]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Input)]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.Int)]
        public int Id { get; set; }

        public string  UserName { get; set; }

        [StoredProcAttributes.Name("LastName")]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Input)]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        public string  LastName { get; set; }

        [StoredProcAttributes.Name("FirstName")]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Input)]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        public string FirstName { get; set; }

        [StoredProcAttributes.Name("Email")]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Input)]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        public string Email { get; set; }


       
    }
}