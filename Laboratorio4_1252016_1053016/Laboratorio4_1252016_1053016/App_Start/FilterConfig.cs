﻿using System.Web;
using System.Web.Mvc;

namespace Laboratorio4_1252016_1053016
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
