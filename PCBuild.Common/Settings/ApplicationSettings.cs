using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PCBuild.Common.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string BaseUrl { get; set; }
        public string ConnectionString { get; set; }

        public ApplicationSettings() {

            BaseUrl = Get("BaseUrl");
            ConnectionString = Get("ConnectionString");
        }

        public string Get(string itemName)
        {
            try
            {
                return ConfigurationManager.AppSettings[itemName];
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
