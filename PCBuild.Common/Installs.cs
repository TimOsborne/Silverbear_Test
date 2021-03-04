using Ninject.Modules;
using PCBuild.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuild.Common
{
    public class Installs : NinjectModule
    {

        public override void Load()
        {
            Bind<IApplicationSettings>().To(typeof(ApplicationSettings)).InSingletonScope();
        }
    }
}
