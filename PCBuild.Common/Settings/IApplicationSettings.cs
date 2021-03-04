using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuild.Common.Settings
{
    public interface IApplicationSettings
    {
        string BaseUrl { get; set; }
        string ConnectionString { get; set; }

    }
}

