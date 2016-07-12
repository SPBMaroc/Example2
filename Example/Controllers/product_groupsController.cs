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
    public class product_groupsController : Controller
    {
        private CrudDBEntities db = new CrudDBEntities();

        // GET: product_groups
        public async Task<ActionResult> Index()
        {
            return View(await db.product_groups.ToListAsync());
        }

        // GET: product_groups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_groups product_groups = await db.product_groups.FindAsync(id);
            if (product_groups == null)
            {
                return HttpNotFound();
            }
            return View(product_groups);
        }

        // GET: product_groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: product_groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_product_group,id_product_group_source,code,label,description,id_ref_application")] product_groups product_groups)
        {
            if (ModelState.IsValid)
            {
                db.product_groups.Add(product_groups);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product_groups);
        }

        // GET: product_groups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_groups product_groups = await db.product_groups.FindAsync(id);
            if (product_groups == null)
            {
                return HttpNotFound();
            }
            return View(product_groups);
        }

        // POST: product_groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_product_group,id_product_group_source,code,label,description,id_ref_application")] product_groups product_groups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_groups).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product_groups);
        }

        // GET: product_groups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_groups product_groups = await db.product_groups.FindAsync(id);
            if (product_groups == null)
            {
                return HttpNotFound();
            }
            return View(product_groups);
        }

        // POST: product_groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            product_groups product_groups = await db.product_groups.FindAsync(id);
            db.product_groups.Remove(product_groups);
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
