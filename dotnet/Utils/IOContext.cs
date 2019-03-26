using Microsoft.AspNetCore.Http;

namespace service.Utils
{
    public class IOContext
    {
        public HttpContext HttpContext { get; set; }

        public string Account
        {
            get { return HttpContext.Request.Headers["x-vtex-account"]; }
        }

        public string Workspace
        {
            get { return HttpContext.Request.Headers["x-vtex-workspace"]; }
        }

        public string AuthToken
        {
            get { return HttpContext.Request.Headers["x-vtex-credential"]; }
        }

        public string Caller
        {
            get { return HttpContext.Request.Headers["x-vtex-caller"]; }
        }
    }
}