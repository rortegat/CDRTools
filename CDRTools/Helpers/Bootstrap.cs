using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace CDRTools.Helpers
{
    public class Bootstrap
    {
        public const string BundleBase = "~/Content/css/";
        public static List<string> Themes { get; set; }

        static Bootstrap()
        {
            var themesFolder = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Content", "Themes");
            Themes = new List<string>(Directory.GetDirectories(themesFolder).Select(x => Path.GetFileName(x)));
        }

        public static string Bundle(string themename)
        {
            return BundleBase + themename;
        }
    }
}