using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using WebApplication1.App_Start;
using System.Web.Mvc;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());
        }
    }
}
