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
    public class usersController : Controller
    {
        private CrudDBEntities db = new CrudDBEntities();

        // GET: users
        public async Task<ActionResult> Index()
        {
            var users = db.users.Include(u => u.profiles);
            return View(await users.ToListAsync());
        }

        // GET: users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = await db.users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            ViewBag.id_profile = new SelectList(db.profiles, "id_profile", "id_profile");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_user,trigram,first_name,last_name,type,password,title,email,activation_date,expiration_date,id_profile")] users users)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(users);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_profile = new SelectList(db.profiles, "id_profile", "id_profile", users.id_profile);
            return View(users);
        }

        // GET: users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = await db.users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_profile = new SelectList(db.profiles, "id_profile", "code", users.id_profile);
            return View(users);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_user,trigram,first_name,last_name,type,password,title,email,activation_date,expiration_date,id_profile")] users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_profile = new SelectList(db.profiles, "id_profile", "code", users.id_profile);
            return View(users);
        }

        // GET: users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = await db.users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            users users = await db.users.FindAsync(id);
            db.users.Remove(users);
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
