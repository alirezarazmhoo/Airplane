using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirAjencyy.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
		[Authorize]
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return View();
        }
    }
}