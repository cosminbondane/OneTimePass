using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OneTimePass.Util
{
    public static class Config
    {
        public static int PasswordExpiration
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PasswordExpiration"] ?? "30");
            }
        }
    }
}