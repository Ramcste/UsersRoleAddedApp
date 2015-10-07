using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserRolesAddedApp.Helpers
{
    public class Utilities
    {
        public static object JsonOkObject(object obj)
        {
            var result = new
            {
                status = "ok",
                data = obj
            };
            return result;
        }

        public static object JsonErrorObject(string Errormsg)
        {
            var result = new
            {
                status = "error",
                data = Errormsg
            };
            return result;
        }
    }
}