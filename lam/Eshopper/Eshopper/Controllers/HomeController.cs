using Eshopper.Commom;
using Eshopper.Models.Dao;
using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            DBModels db = new DBModels();

            return View(db);
        }

       
    }
}