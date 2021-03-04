using PCBuild.Common.Settings;
using PCBuild.Web.Helpers;
using PCBuild.Fatory.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;


namespace PCBuild.Controllers
{
    [AllowAnonymous]
    public class PublicPageController : ApiController 
    {

        private readonly IApplicationSettings _applicationSettings;

        public PublicPageController(IApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }


        [HttpGet]
        public async Task<IHttpActionResult> Index()
        {
            var replaces = new Dictionary<string, string> {
                { "%BASE_URL%" , _applicationSettings.BaseUrl }
            };

            return await Task.Run(() => { return new FileHtmlResult(Request, "~/Assets/index.html", replaces); }); 
        }
    }
}