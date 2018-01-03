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
    }
}
