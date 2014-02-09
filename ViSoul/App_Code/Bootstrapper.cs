using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using Umbraco.Core;
using System.Web.Routing;

namespace UmbracoBP.App_Code
{
    public class Bootstrapper : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {

        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            //System.Web.Optimization.BundleTable.Bundles
            //BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //    "~/Scripts/jquery-{version}.js"));

            //BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //    "~/Scripts/jquery.unobtrusive*",
            //    "~/Scripts/jquery.validate*"));

            //BundleTable.Bundles.Add(new ScriptBundle("~/bundles/fbconnect").Include(
            //    "~/Scripts/myfbconnect*"));
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}