using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GettingStarted.Controllers
{
    public class RoutesController : Controller
    {
        public string PrintHelloWorld()
        {
            return "Hello, IO!";
        }
    }
}
