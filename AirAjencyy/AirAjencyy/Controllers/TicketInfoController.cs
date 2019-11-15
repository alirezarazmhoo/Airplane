using AirAjencyy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirAjencyy.Controllers
{
    public class TicketInfoController : Controller
    {
		private AirAjencyyContext db = new AirAjencyyContext();

		// GET: TicketInfo
		public ActionResult Index(int? id)
        {
			
				var item = db.PlaneTickets.Where(s => s.Id == id).FirstOrDefault();
			

            return View(item);
        }


		public ActionResult AcceptAndCountinue(int? id)
		{
			
			ViewBag.IdTicket = id;
			return View();
		}

		public ActionResult CustomerFactor(Factors factors)
		{
			

			factors.CreateDate = DateTime.Now;
			db.Factors.Add(factors);
			db.SaveChanges();

			return RedirectToAction(nameof(Success));

		}

		public ActionResult Success()
		{
			return View();
		}

		




	}
}