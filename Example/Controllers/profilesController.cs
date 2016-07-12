using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Example
{
    [Authorize]
    public class profilesController : Controller
    {
        private CrudDBEntities db = new CrudDBEntities();

        // GET: profiles
        public async Task<ActionResult> Index()
        {
            var profiles = db.profiles.Include(p => p.product_groups);
            return View(await profiles.ToListAsync());
        }

        // GET: profiles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            profiles profiles = await db.profiles.FindAsync(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        // GET: profiles/Create
        public ActionResult Create()
        {
            ViewBag.id_product_group = new SelectList(db.product_groups, "id_product_group", "code");
            return View();
        }

        // POST: profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_profile,id_product_group,code,label,profile_level,description")] profiles profiles)
        {
            if (ModelState.IsValid)
            {
                db.profiles.Add(profiles);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_product_group = new SelectList(db.product_groups, "id_product_group", "code", profiles.id_product_group);
            return View(profiles);
        }

        // GET: profiles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            profiles profiles = await db.profiles.FindAsync(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_product_group = new SelectList(db.product_groups, "id_product_group", "code", profiles.id_product_group);
            return View(profiles);
        }

        // POST: profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_profile,id_product_group,code,label,profile_level,description")] profiles profiles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profiles).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_product_group = new SelectList(db.product_groups, "id_product_group", "code", profiles.id_product_group);
            return View(profiles);
        }

        // GET: profiles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            profiles profiles = await db.profiles.FindAsync(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        // POST: profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            profiles profiles = await db.profiles.FindAsync(id);
            db.profiles.Remove(profiles);
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
