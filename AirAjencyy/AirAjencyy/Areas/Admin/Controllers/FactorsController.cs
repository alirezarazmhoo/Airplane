using AirAjencyy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirAjencyy.Areas.Admin.Controllers
{
    public class FactorsController : Controller
    {
		// GET: Admin/Factors
		private AirAjencyyContext db = new AirAjencyyContext();

		public ActionResult Index(string FromDate,string ToDate)
        {
			object listFactors = new object();
			if (FromDate!=null && ToDate != null && FromDate !="" && ToDate !="")
			{
				DateTime date1 = Utility.DateChanger.ToGeorgianDateTime(FromDate);
				DateTime date2 = Utility.DateChanger.ToGeorgianDateTime(ToDate);


				 listFactors = db.Factors.Where(s=>s.CreateDate >= date1  && s.CreateDate < date2).ToList();
				return View(listFactors);

			}


			listFactors = db.Factors.ToList();
			

            return View(listFactors);
        }
    }
}