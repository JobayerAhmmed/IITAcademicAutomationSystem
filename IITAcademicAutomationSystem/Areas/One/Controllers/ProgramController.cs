using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;

namespace IITAcademicAutomationSystem.Areas.One.Controllers
{
    public class ProgramController : Controller
    {
        //private AppContext db = new AppContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: One/Program
        public ActionResult Index()
        {
            var programs = unitOfWork.ProgramRepository.Get();
            return View(programs.ToList());
        }

        // GET: One/Program/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = unitOfWork.ProgramRepository.GetByID(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // GET: One/Program/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: One/Program/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProgramName")] Program program)
        {
            if (ModelState.IsValid)
            {
                //db.Programs.Add(program);
                //db.SaveChanges();
                unitOfWork.ProgramRepository.Insert(program);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(program);
        }

        // GET: One/Program/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Program program = db.Programs.Find(id);
            Program program = unitOfWork.ProgramRepository.GetByID(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // POST: One/Program/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProgramName")] Program program)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(program).State = EntityState.Modified;
                //db.SaveChanges();
                unitOfWork.ProgramRepository.Update(program);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(program);
        }

        // GET: One/Program/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Program program = db.Programs.Find(id);
            Program program = unitOfWork.ProgramRepository.GetByID(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // POST: One/Program/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Program program = db.Programs.Find(id);
            //db.Programs.Remove(program);
            //db.SaveChanges();
            unitOfWork.ProgramRepository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
