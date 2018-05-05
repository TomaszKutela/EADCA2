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
using System.Data.Entity.Infrastructure;

namespace CA2_IMeet.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private BookingContext db = new BookingContext();

        // GET: Booking/ViewAllBookings
        public ActionResult ViewAllBookings()
        {
            var bookings = db.Bookings.Include(b => b.MeetingRoom);
            return View(bookings.ToList());
        }

        // GET: Booking/ViewMyBookings
        public ActionResult ViewMyBookings(string userId)
        {
            //****Fix route prefixing in MVC!!!!
            //*******check here how to get current DATE!!!!!
            var bookings = db.Bookings.Where(b => b.UserId == userId && b.Date >= DateTime.Now).Include(b => b.MeetingRoom);
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
        public ActionResult Create(DateTime? pickedDate, DateTime? pickedStartTime, DateTime? pickedEndTime)
        {
            //ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name");
            //if pickedDate != null, check room availability then render partial view?
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingReference, RoomId, Date, Start_DateTime, End_DateTime")] Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //make sure room is still free
                    foreach (Booking b in db.Bookings)
                    {
                        if (!b.IsValidBooking(booking))
                        {
                            ModelState.AddModelError("", "This room is not available any longer for booking. Please try to make another booking.");
                            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", booking.RoomId);
                            return View(booking);
                        }
                    }
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("ViewAllBookings");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again, and if the problem persists, contact your system admistrator");
            }
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", booking.RoomId);
            return View(booking);
        }


        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            SelectList roomslist = new SelectList(db.MeetingRooms.ToList(), "RoomId", "Name", db.MeetingRooms);
            ViewData["Names"] = roomslist;

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

        // POST: Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookingToUpdate = db.Bookings.Find(id);

            if (TryUpdateModel(bookingToUpdate, "", 
                new string[] { "MeetingReference", "RoomId", "Date", "Start_DateTime", "End_DateTime", "UserId" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("ViewAllBookings");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again. If problem persists, contact your system administrator.");
                }

            }
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", bookingToUpdate.RoomId);
            return View(bookingToUpdate);
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
                ViewBag.ErrorMessage = "Delete failed. Please try again. If problem persists, contact your system administrator.";
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
            return RedirectToAction("ViewAllBookings");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //CHECK THIS!!
        private List<MeetingRoom> FindAvailableRooms(DateTime date, DateTime startTime, DateTime endTime)
        {
            List<MeetingRoom> availableRooms = new List<MeetingRoom>();
            IEnumerable<Booking> filteredBookings = db.Bookings.Where(b => b.Date == date);
            foreach (MeetingRoom r in db.MeetingRooms)
            {
                filteredBookings = filteredBookings.Where(b => b.RoomId == r.RoomId);
                foreach (Booking b in filteredBookings)
                {
                    if ((b.Start_DateTime > endTime) || (b.End_DateTime < startTime))
                    {
                        availableRooms.Add(r);
                    }
                }  
            }
            return availableRooms;
        }
    }
}
