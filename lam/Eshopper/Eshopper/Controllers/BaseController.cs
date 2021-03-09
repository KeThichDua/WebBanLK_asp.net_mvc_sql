﻿using Eshopper.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eshopper.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (NguoiDungLogin)Session[CommomConstants.NguoiDungSession];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index"/*, Area = "Login"*/ }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}