using AirAjencyy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AirAjencyy.Controllers
{
    public class LoginController : Controller
    {
		private AirAjencyyContext db = new AirAjencyyContext();
		// GET: Login
		public ActionResult Index()
        {
            return View();
        }
		[HttpGet]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();

			return Redirect("/Home/Index");
		}

		public ActionResult Register(AdminUser user)
		{
			if(user.Password == null || user.UserName == null )

			{
				TempData["ErrorLoging"] = "رمز عبور یا نام کاربری خالی است";
				return RedirectToAction(nameof(Index));
			}
			if(!db.AdminUser.Any(s=>s.UserName == user.UserName) && db.AdminUser.Any(s=>s.Password == user.Password))
			{
				TempData["Incorrect"] = "نام کاربری یا رمز عبور صحیح نیست";
				return RedirectToAction(nameof(Index));

			}
			if (db.AdminUser.Any(s => s.UserName == user.UserName) && db.AdminUser.Any(s => s.Password == user.Password))
			{
            FormsAuthentication.SetAuthCookie(user.UserName, false);


				return Redirect("/Admin/AdminHome/Index");

			}
			return RedirectToAction(nameof(Index));


		}
	}
}