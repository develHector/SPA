using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SPAApplication.Models;

namespace SPAApplication.Controllers
{

    // No hacerle mucho caso a este código, es un mal diseño, hay mucho código de base de datos

    // Esta clase se creó con el default Scafolding

    // Controller tiene todos los helpers que hacen que todo funcione, por ejemplo, el View()

    
    public class AlbumsController : Controller
    {
        private SPAApplicationContext db = new SPAApplicationContext();

        public ActionResult DisplayByArtist(int artistID)
        {
            // *** imagine code here
            return this.View(); // Esto hereda de Controller, va a buscar el nombre y los datos
        }
        

        // GET: Albums
        public ActionResult Index()
        {
            // Aquí es donde se van a desplegar los Albums

            // Let's get the model, esto es Entity Framework ayudado con nuestro Context, dame los albumes y conviértelos a una lista DotNet
            var albums = db.Albums.ToList() ;

            // combine the model with the view and return
            return View(albums);
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {

            // Aquí es donde se van a desplegar sólo un item
            // El siguiente código usa CodeFirst entity framework

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // *** Este siguiente es el que desplegará la forma

        // GET: Albums/Create
        // Display form
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // *** Este es el que aceptará el input

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Accept input        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumID,Title")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumID,Title")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
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
