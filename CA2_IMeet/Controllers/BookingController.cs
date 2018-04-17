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
    public class BookingController : Controller
    {
        private BookingContext db = new BookingContext();

        // GET: Booking
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.MeetingRoom);
            return View(bookings.ToList());
        }

        // GET: Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name");
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingReference,RoomId,Date,Start_DateTime,End_DateTime,")] Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again,and if the problem persists see your system admistrator")
            }
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", booking.RoomId);
            return View(booking);
        }



        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookingToUpdate = db.Bookings.Find(id);
           
            if (TryUpdateModel(bookingToUpdate, "", new string[] {"Booking ID: ","Date: ", "Start Time: ", "End Time: ",}))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(DataException)
                {
                    ModelState.AddModelError("","Unable to save changes.Try again. If problem persists, see your system administrator")
                }
                
            }
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", bookingToUpdate.RoomId);
            return View(bookingToUpdate);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,MeetingReference,RoomId,Date,Start_DateTime,End_DateTime,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", booking.RoomId);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again. If problem persists see your system administrator";
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
