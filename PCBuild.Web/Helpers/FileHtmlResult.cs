using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PCBuild.Web.Helpers
{
    public class FileHtmlResult : IHttpActionResult
    {

        private string _name;
        private HttpRequestMessage _request;
        private Dictionary<string, string> _replaces;

        public FileHtmlResult(HttpRequestMessage request, string name, Dictionary<string, string> replaces)
        {
            _name = name;
            _request = request;
            _replaces = replaces;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(GetResponseMessage(_request, _name, _replaces));
        }
        
        public static HttpResponseMessage GetResponseMessage(HttpRequestMessage request, string name,
            Dictionary<string, string> replaces)
        {
            string html;
            try
            {
                var file = HttpContext.Current.Server.MapPath(name);
                html = File.ReadAllText(file);
            }
            catch (DirectoryNotFoundException)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            replaces.ForEach(x => html = html.Replace((string)x.Key, x.Value));

            return new HttpResponseMessage()
            {
                Content = new StringContent(html, Encoding.UTF8, "text/html")
            };
        }

    }
}