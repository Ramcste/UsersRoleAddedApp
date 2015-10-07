using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstStoredProcs;

namespace UserRolesAddedApp.Models.DAL.Input
{
    public class UserRolesInput
    {

        [StoredProcAttributes.Name("UserId")]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Input)]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.Int)]
        public int UserId { get; set; }

        [StoredProcAttributes.Name("RoleId")]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Input)]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.Int)]
        public int RoleId { get; set; }

        [StoredProcAttributes.Name("MsgText")]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Output)]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        [StoredProcAttributes.Size(100)]
        public string MsgText { get; set; }



        [StoredProcAttributes.Name("MsgType")]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Output)]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        [StoredProcAttributes.Size(10)]
        public string MsgType { get; set; }


    }
}