using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeFirstStoredProcs;
using UserRolesAddedApp.Models.DAL.Input;
using UserRolesAddedApp.Models.DAL.Output;

namespace UserRolesAddedApp.Models.DAL
{
    public class StoredProcContext:UserRolesAddedDBContext
    {
       
        [StoredProcAttributes.Name("[Register.Complete]")]

        public StoredProc<UserRolesInput> GetRegisterCompleteProc { get; set; } 



        [StoredProcAttributes.Name("[Register.Complete_ReasultSheet]")]

        [StoredProcAttributes.ReturnTypes(typeof(User))]

        public StoredProc  GetRegisterCompleteResultSheetProc { get; set; }


        [StoredProcAttributes.Name("IndividualUser_Info")]

        [StoredProcAttributes.ReturnTypes(typeof(User))]

        public StoredProc <IndividualUser_Input> GetIndividualUserInfo { get; set; } 


        [StoredProcAttributes.Name("UpdateAspNetUser_Set")]
        public StoredProc<User> GetUpdateAspNetUserProc { get; set; } 


        [StoredProcAttributes.Name("UploadImage_Complete")]
        public StoredProc GetUploadImageCompleteStoredProc { get; set; }


        public StoredProcContext()
        {
            this.InitializeStoredProcs();
        }

    }

 
}