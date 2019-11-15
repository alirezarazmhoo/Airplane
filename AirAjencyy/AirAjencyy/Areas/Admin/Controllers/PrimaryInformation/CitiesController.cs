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
    public class CitiesController : Controller
    {
        private AirAjencyyContext db = new AirAjencyyContext();

        // GET: Admin/Cities
        public async Task<ActionResult> Index(string name)
        {
			if(name!=null && name!="")
			{
				var _cities = db.Cities.Include(c => c.Country).Where(s=>s.Name.Contains(name) || s.Country.Name.Contains(name));
				return View(await _cities.ToListAsync());

			}

			var cities = db.Cities.Include(c => c.Country);
            return View(await cities.ToListAsync());
        }


        // GET: Admin/Cities/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CountryID")] City city)
        {
            if (ModelState.IsValid)
            {
				var isExist = db.Cities.Any(s => s.Name == city.Name);
				if (isExist)
				{
					TempData["ErrorCity"] = "نام استان تکراری است";
					return RedirectToAction(nameof(Create));
				}

                db.Cities.Add(city);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.Countries, "Id", "Name", city.CountryID);
            return View(city);
        }

        // GET: Admin/Cities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countries, "Id", "Name", city.CountryID);
            return View(city);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CountryID")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Countries, "Id", "Name", city.CountryID);
            return View(city);
        }

    
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Admin/Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            City city = await db.Cities.FindAsync(id);
            db.Cities.Remove(city);
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
