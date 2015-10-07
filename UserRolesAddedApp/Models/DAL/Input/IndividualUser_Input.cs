using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstStoredProcs;

namespace UserRolesAddedApp.Models.DAL.Input
{
    public class IndividualUser_Input
    {
        [StoredProcAttributes.Name("Id")]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Input)]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.Int)]
        public int Id { get; set; }
    }
}