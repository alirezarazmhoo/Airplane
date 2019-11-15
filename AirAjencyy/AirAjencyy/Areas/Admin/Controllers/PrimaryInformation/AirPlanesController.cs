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

namespace AirAjencyy.Areas.Admin.Controllers.PrimaryInformation
{
    public class AirPlanesController : Controller
    {
        private AirAjencyyContext db = new AirAjencyyContext();

        // GET: Admin/AirPlanes
        public async Task<ActionResult> Index(string name)
        {
			if(name !=null && name != "")
			{
				return View(await db.AirPlanes.Where(s=>s.airPlaneModel.Contains(name)).ToListAsync());

			}


			return View(await db.AirPlanes.ToListAsync());
        }

 

        // GET: Admin/AirPlanes/Create
        public ActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,airPlaneModel,ImageUrl")] AirPlane airPlane, HttpPostedFileBase Main_Image)
        {
            if (ModelState.IsValid)
            {
				if (Main_Image != null)
				{



					if (!(Main_Image.ContentType == "image/jpeg" || Main_Image.ContentType == "image/png" || Main_Image.ContentType == "image/bmp"))
					{
						TempData["ErrorAirPlane"] = "نوع تصویر غیر قابل قبول است";
						return RedirectToAction(nameof(Create));
					}

					var name = Guid.NewGuid().ToString().Replace('-', '0') + "." + Main_Image.FileName.Split('.')[1];
					var imageUrl = "/Upload/Plane/" + name;
					string path = Server.MapPath(imageUrl);
					Main_Image.SaveAs(path);
					airPlane.ImageUrl = name;
				}
				db.AirPlanes.Add(airPlane);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(airPlane);
        }

        // GET: Admin/AirPlanes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPlane airPlane = await db.AirPlanes.FindAsync(id);
            if (airPlane == null)
            {
                return HttpNotFound();
            }
            return View(airPlane);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,airPlaneModel,ImageUrl")] AirPlane airPlane , HttpPostedFileBase Main_Image)
        {
            if (ModelState.IsValid)
            {
				var item = db.AirPlanes.Where(s => s.Id == airPlane.Id).FirstOrDefault();

				if (Main_Image != null)
				{
					if(item.ImageUrl != null)
					{
						System.IO.File.Delete(Server.MapPath(String.Concat("~/Upload/Plane/", item.ImageUrl)));

					}



					if (!(Main_Image.ContentType == "image/jpeg" || Main_Image.ContentType == "image/png" || Main_Image.ContentType == "image/bmp"))
					{
						TempData["ErrorAirPlane"] = "نوع تصویر غیر قابل قبول است";
						return RedirectToAction(nameof(Create));
					}

					var name = Guid.NewGuid().ToString().Replace('-', '0') + "." + Main_Image.FileName.Split('.')[1];
					var imageUrl = "/Upload/Plane/" + name;
					string path = Server.MapPath(imageUrl);
					Main_Image.SaveAs(path);
					item.ImageUrl = name;
				}

				item.airPlaneModel = airPlane.airPlaneModel;
				

		
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(airPlane);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPlane airPlane = await db.AirPlanes.FindAsync(id);
            if (airPlane == null)
            {
                return HttpNotFound();
            }
            return View(airPlane);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AirPlane airPlane = await db.AirPlanes.FindAsync(id);
			var haspic =await db.AirPlanes.Where(s => s.Id == id).FirstOrDefaultAsync();
			if(haspic.ImageUrl != null)
			{
				System.IO.File.Delete(Server.MapPath(String.Concat("~/Upload/Plane/",haspic.ImageUrl)));
			}

            db.AirPlanes.Remove(airPlane);
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
