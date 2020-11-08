﻿using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using ProjectBackAndFrontend.Core.Config;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProjectBackAndFrontend.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            NinjectModule registrations = new DependenciesConfig();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
