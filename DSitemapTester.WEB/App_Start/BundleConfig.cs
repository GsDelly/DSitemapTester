using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace DSitemapTester
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var googleChartsCdnPath = "https://www.gstatic.com/charts/loader.js";

            bundles.Add(new StyleBundle("~/Content/MainStyles")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/Site.css")
                .Include("~/Content/icons/*.png")
                .Include("~/Content/AdminLTE/AdminLTE.css")
                .Include("~/Content/AdminLTE/skins/skin-yellow.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery-ui")
                .Include("~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/AdminLTETemplate")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/AdminLTE/app.js"));

            bundles.Add(new ScriptBundle("~/Scripts/canvasjs")
                .Include("~/Scripts/canvasJS/canvasjs.min.js")
                .Include("~/Scripts/canvasJS/jquery.canvasjs.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/GoogleCharts", googleChartsCdnPath)
              .Include("~/Scripts/loader.js"));
        }
    }
}
