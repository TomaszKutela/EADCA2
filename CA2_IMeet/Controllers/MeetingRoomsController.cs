using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CA2_IMeet.DAL;
using CA2_IMeet.Models;

namespace CA2_IMeet.Controllers
{
    public class MeetingRoomsController : Controller
    {
        private BookingContext db = new BookingContext();

        // GET: MeetingRooms
        public ActionResult Index()
        {
            return View(db.MeetingRooms.ToList());
        }

        // GET: MeetingRooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRoom meetingRoom = db.MeetingRooms.Find(id);
            if (meetingRoom == null)
            {
                return HttpNotFound();
            }
            return View(meetingRoom);
        }

        // GET: MeetingRooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeetingRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,Name,Size,Location,Equipment")] MeetingRoom meetingRoom)
        {
            if (ModelState.IsValid)
            {
                db.MeetingRooms.Add(meetingRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetingRoom);
        }

        // GET: MeetingRooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRoom meetingRoom = db.MeetingRooms.Find(id);
            if (meetingRoom == null)
            {
                return HttpNotFound();
            }
            return View(meetingRoom);
        }

        // POST: MeetingRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomId,Name,Size,Location,Equipment")] MeetingRoom meetingRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingRoom);
        }

        // GET: MeetingRooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRoom meetingRoom = db.MeetingRooms.Find(id);
            if (meetingRoom == null)
            {
                return HttpNotFound();
            }
            return View(meetingRoom);
        }

        // POST: MeetingRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingRoom meetingRoom = db.MeetingRooms.Find(id);
            db.MeetingRooms.Remove(meetingRoom);
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
