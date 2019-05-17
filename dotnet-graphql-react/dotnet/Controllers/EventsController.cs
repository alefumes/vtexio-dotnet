using Microsoft.AspNetCore.Mvc;

namespace GettingStarted.Controllers
{
    public class EventsController : Controller
    {
        public string OnAppsLinked(string account, string workspace)
        {
            return $"OnAppsLinked event detected for {account}/{workspace}";
        } 
    }
}
