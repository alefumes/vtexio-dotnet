using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace service.Controllers
{
    public class RoutesController : Controller
    {
        public string PrintHelloWorld()
        {
            return "Hello, IO!";
        } 

        public string PrintHello(string name)
        {
            return $"Hello, {name}!";
        } 
      
        public string PrintPrivateValue(string account, string workspace) 
        {
            return $"My private value! Account: {account} | Workspace: {workspace}";
        }

        public string ApiDefault()
        {
            return "This is the default API response";
        }

        public string Default()
        {
            return "This is the default response";
        }
    }
}
