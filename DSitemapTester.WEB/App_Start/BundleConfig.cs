using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace DSitemapTester.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/MainStyles")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/AdminLTE/AdminLTE.css")
                .Include("~/Content/AdminLTE/skins/skin-blue.css"));

            bundles.Add(new StyleBundle("~/Content/DataTableStyles")
                .Include("~/Content/DataTables/css/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery-ui")
                .Include("~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/AdminLTETemplate")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/AdminLTE/app.js"));
        }
}
