using Eshopper.Commom;
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
            //var session = (NguoiDungLogin)Session[CommomConstants.NguoiDungSession];
            //if (session == null)
            //{
            //    session = (NguoiDungLogin)Session[CommomConstants.AdminSession];
            //    if(session == null)
            //    {
            //        filterContext.Result = Redirect("/Login");
            //                            //new RedirectToRouteResult(new
            //                            //RouteValueDictionary(new { controller = "Login", action = "Index"}));
            //    }
                
               
            //}
            //base.OnActionExecuting(filterContext);

            var session = (NguoiDungLogin)Session[CommomConstants.NguoiDungSession];
            if (session == null)
            {
                
                    filterContext.Result = Redirect("/Login");
                    //new RedirectToRouteResult(new
                    //RouteValueDictionary(new { controller = "Login", action = "Index"}));
                


            }
            base.OnActionExecuting(filterContext);
        }
    }
}