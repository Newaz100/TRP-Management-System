using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRPManagement.EF;

namespace TRPManagement.Controllers
{
    public class ProgramController : Controller
    {
        private TRMdbEntities db = new TRMdbEntities();

        // GET: Program List (Grouped by Channels)
        public ActionResult ProgramList()
        {
            var channelsWithPrograms = db.Channels.Include("Programs").ToList();
            return View(channelsWithPrograms);
        }

        // GET: Create Program
        public ActionResult ProgramCreate()
        {
            ViewBag.Channels = db.Channels.ToList();
            return View();
        }

        // POST: Create Program
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProgramCreate(Program program)
        {
            if (ModelState.IsValid)
            {
                db.Programs.Add(program);
                db.SaveChanges();
                TempData["Success"] = "Program added successfully.";
                return RedirectToAction("ProgramList");
            }
            ViewBag.Channels = db.Channels.ToList();
            return View(program);
        }

        // GET: Edit Program
        public ActionResult ProgramEdit(int id)
        {
            var program = db.Programs.Find(id);
            if (program == null) return HttpNotFound();

            ViewBag.Channels = db.Channels.ToList();
            return View(program);
        }

        // POST: Edit Program
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProgramEdit(Program program)
        {
            if (ModelState.IsValid)
            {
                db.Entry(program).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Program updated successfully.";
                return RedirectToAction("ProgramList");
            }
            ViewBag.Channels = db.Channels.ToList();
            return View(program);
        }

        // GET: Delete Program
        public ActionResult Delete(int id)
        {
            var program = db.Programs.Find(id);
            if (program == null)
            {
                TempData["Error"] = "Program not found.";
                return RedirectToAction("ProgramList");
            }
            return View(program);
        }

        // POST: Confirm Delete Program
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var program = db.Programs.Find(id);
            if (program != null)
            {
                db.Programs.Remove(program);
                db.SaveChanges();
                TempData["Success"] = "Program deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Program not found.";
            }
            return RedirectToAction("ProgramList");
        }
    }
}