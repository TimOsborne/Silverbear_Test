using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using HibernatingRhinos.Profiler.Appender.NHibernate;


namespace PCBuild.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            NHibernateProfiler.Initialize();

        }
    }
}
