using System.Web.Mvc;

namespace WebApplication1.App_Start
{
    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Services/{0}.cshtml" // Add this line for Services views
            };

            PartialViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Services/{0}.cshtml" // Add this line for Services partial views
            };
        }
    }
}