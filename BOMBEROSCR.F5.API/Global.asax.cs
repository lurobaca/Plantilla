//using Plantilla.API.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Plantilla.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			//AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
			AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			//SE ESPECIFICAN LOS FORMATOS DE LAS FECHAS DESEADOS
			CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
			newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
			newCulture.DateTimeFormat.LongDatePattern = "dddd, d 'de' MMMM 'de' yyyy";
			newCulture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
			newCulture.DateTimeFormat.ShortTimePattern = "HH:mm";
			newCulture.DateTimeFormat.DateSeparator = "/";
			Thread.CurrentThread.CurrentCulture = newCulture;
		}
		protected void Application_Error(object sender, EventArgs e)
		{
			Exception ex = Server.GetLastError();
			string err = ex.ToString();
		}
	}
}
