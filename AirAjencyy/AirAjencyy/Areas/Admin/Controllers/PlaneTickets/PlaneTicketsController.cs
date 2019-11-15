using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AirAjencyy.Models;
using AirAjencyy.Utility;

namespace AirAjencyy.Areas.Admin.Controllers.PlaneTickets
{
    public class PlaneTicketsController : Controller
    {
        private AirAjencyyContext db = new AirAjencyyContext();

        // GET: Admin/PlaneTickets
        public async Task<ActionResult> Index(string hasSearch)
        {
			if(hasSearch !=null && hasSearch != "")
			{
				long price = 0;
			
				if (Request["price"] != "" && Request["price"] != "")
				{
				 price =Convert.ToInt64(Request["price"]);

				}
				int Type = Convert.ToInt32(Request["Type"]);

			
				string date = Convert.ToString(Request["TakeOffDate"]);
				DateTime TakeOffDate = Utility.DateChanger.ToGeorgianDateTime(date);
				int OriginCountryCode = Convert.ToInt32(Request["OriginCountryCode"]);
				int OriginCityCode = Convert.ToInt32(Request["OriginCityCode"]);
				int DestinyCountryCode = Convert.ToInt32(Request["DestinyCountryCode"]);
				int DestinyCityCode = Convert.ToInt32(Request["DestinyCityCode"]);



				var _planeTickets =await db.PlaneTickets.Include(p => p.CountryOrigin).Where(s=>s.Price == price || s.Type == Type || s.TakeOffDate == TakeOffDate || s.CountryOriginID == OriginCountryCode || s.CityOriginCode == OriginCityCode || s.CountryDestinationCode == DestinyCountryCode || s.CityDestinationID == DestinyCityCode).ToListAsync();


				return View(_planeTickets);



			}


			var planeTickets = db.PlaneTickets.Include(p => p.CountryOrigin);
            return View(await planeTickets.ToListAsync());
        }

    
        // GET: Admin/PlaneTickets/Create
        public ActionResult Create()
        {
            ViewBag.CountryOriginID = new SelectList(db.Countries, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Type,TakeOffDate,ImageUrl,CountryOriginID,CityDestinationID,Descriptions,Price,CountryDestinationCode,CityOriginCode,AirPlaneCode,TakeOffTime")] PlaneTicket planeTicket, HttpPostedFileBase Main_Image,string mydate)
        {
			if(Main_Image == null)
			{
				TempData["AirTicketError"] = "تصویری انتخاب نشده";
				return RedirectToAction(nameof(Create));
			}
			if(Main_Image != null)
			{
				if (!(Main_Image.ContentType == "image/jpeg" || Main_Image.ContentType == "image/png" || Main_Image.ContentType == "image/bmp"))
				{
					TempData["ErrorAirPlane"] = "نوع تصویر غیر قابل قبول است";
					return RedirectToAction(nameof(Create));
				}
				var name = Guid.NewGuid().ToString().Replace('-', '0') + "." + Main_Image.FileName.Split('.')[1];
				var imageUrl = "/Upload/Tickets/" + name;
				string path = Server.MapPath(imageUrl);
				Main_Image.SaveAs(path);
				planeTicket.ImageUrl = name;
			}

            if (ModelState.IsValid)
            {
				

				DateTime mydatee = DateChanger.ToGeorgianDateTime(mydate);
				planeTicket.TakeOffDate = mydatee;
				planeTicket.TakeOffTime = Convert.ToString(planeTicket.TakeOffTime);
				db.PlaneTickets.Add(planeTicket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryOriginID = new SelectList(db.Countries, "Id", "Name", planeTicket.CountryOriginID);
            return View(planeTicket);
        }

        // GET: Admin/PlaneTickets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaneTicket planeTicket = await db.PlaneTickets.FindAsync(id);
            if (planeTicket == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryOriginID = new SelectList(db.Countries, "Id", "Name", planeTicket.CountryOriginID);
            return View(planeTicket);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Type,TakeOffDate,ImageUrl,CountryOriginID,CityDestinationID,Descriptions,Price,CountryDestinationCode,CityOriginCode")] PlaneTicket planeTicket, HttpPostedFileBase Main_Image, string mydate)
        {
			var item =await db.PlaneTickets.Where(s => s.Id == planeTicket.Id).FirstOrDefaultAsync();
			if (Main_Image != null)
			{
				if(item.ImageUrl != null)
				{
					System.IO.File.Delete(Server.MapPath(String.Concat("~/Upload/Tickets/", item.ImageUrl)));
				}

				if (!(Main_Image.ContentType == "image/jpeg" || Main_Image.ContentType == "image/png" || Main_Image.ContentType == "image/bmp"))
				{
					TempData["ErrorAirPlane"] = "نوع تصویر غیر قابل قبول است";
					return RedirectToAction(nameof(Edit));
				}
				var name = Guid.NewGuid().ToString().Replace('-', '0') + "." + Main_Image.FileName.Split('.')[1];
				var imageUrl = "/Upload/Tickets/" + name;
				string path = Server.MapPath(imageUrl);
				Main_Image.SaveAs(path);
				item.ImageUrl = name;
			}

			if (ModelState.IsValid)
            {
				DateTime mydatee = DateChanger.ToGeorgianDateTime(mydate);
				planeTicket.TakeOffDate = mydatee;


				item.Price = planeTicket.Price;

				item.TakeOffDate = mydatee;
				item.Type = planeTicket.Type;
				item.CountryOriginID = planeTicket.CountryOriginID;
				item.CountryDestinationCode = planeTicket.CountryDestinationCode;
				item.CityDestinationID = planeTicket.CityDestinationID;
				item.CityOriginCode = planeTicket.CityOriginCode;


                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryOriginID = new SelectList(db.Countries, "Id", "Name", planeTicket.CountryOriginID);
            return View(planeTicket);
        }

        // GET: Admin/PlaneTickets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaneTicket planeTicket = await db.PlaneTickets.FindAsync(id);
            if (planeTicket == null)
            {
                return HttpNotFound();
            }
            return View(planeTicket);
        }

        // POST: Admin/PlaneTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
			var haspic = await db.PlaneTickets.Where(s => s.Id == id).FirstOrDefaultAsync();
			if (haspic.ImageUrl != null)
			{
				System.IO.File.Delete(Server.MapPath(String.Concat("~/Upload/Tickets/", haspic.ImageUrl)));
			}

			PlaneTicket planeTicket = await db.PlaneTickets.FindAsync(id);
            db.PlaneTickets.Remove(planeTicket);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
