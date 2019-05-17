using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Logging; 
 
namespace Vtex 
{ 
    public class StartupExtender 
    { 
        // This method is called inside Startup's constructor 
        // You can use it to build a custom configuration 
        public void ExtendConstructor(IConfiguration config, IHostingEnvironment env, ILoggerFactory loggerFactory) 
        { 

        } 
 
        // This method is called inside Startup.ConfigureServices()
        // Note that you don't need to call AddMvc() here 
        public void ExtendConfigureServices(IServiceCollection services) 
        { 

        } 
 
        // This method is called inside Startup.Configure() before calling app.UseMvc()
        public void ExtendConfigureBeforeMvc(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) 
        { 

        } 
 
        // This method is called inside Startup.Configure() after calling app.UseMvc()
        public void ExtendConfigureAfterMvc(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) 
        { 

        } 
    } 
}