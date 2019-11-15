using AirAjencyy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirAjencyy.Controllers
{
	public class HomeController : Controller
	{
		private AirAjencyyContext db = new AirAjencyyContext();

		public ActionResult Index()
		{
			bool IsAdminExist = db.AdminUser.Any();
			if (!IsAdminExist)
			{
				AdminUser user = new AdminUser();

				user.UserName = "Zareyi";
				user.Password = "123456";
				db.AdminUser.Add(user);
				db.SaveChanges();
			}

			return View();
		}


	}
}