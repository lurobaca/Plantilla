﻿using System.Web;
using System.Web.Mvc;

namespace BOMBEROSCR.F5.UI
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}