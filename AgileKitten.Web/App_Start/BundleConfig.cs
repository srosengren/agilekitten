﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace AgileKitten.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif


            bundles.Add(new Bundle("~/bundles/application")
                .Include("~/Scripts/knockout-{version}.js")
                .Include("~/Scripts/knockout.mapping-*")
                .Include("~/Scripts/App/application.js")
                );
        }
    }
}