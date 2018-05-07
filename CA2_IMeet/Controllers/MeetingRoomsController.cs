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
    public class MeetingRoomsController : Controller
    {
        private BookingContext db = new BookingContext();

        // GET: MeetingRooms
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SizeSortParm = sortOrder == "Size" ? "size_desc" : "Size";
            var rooms = from r in db.MeetingRooms
                        select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(r => r.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    rooms = rooms.OrderByDescending(r => r.Name);
                    break;
                case "Size":
                    rooms = rooms.OrderBy(r => r.Size);
                    break;
                case "size_desc":
                    rooms = rooms.OrderByDescending(r => r.Size);
                    break;
                default:
                    rooms = rooms.OrderBy(r => r.Name);
                    break;
            }
            return View(rooms.ToList());
        }

        public ActionResult Details(string keyword)
        {
            if (String.IsNullOrEmpty(keyword))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRoom meetingRoom = db.MeetingRooms.Where(n => n.Name == keyword).First();
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
        public ActionResult Create([Bind(Include = "Name,Size,Location,Equipment")] MeetingRoom meetingRoom)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.MeetingRooms.FirstOrDefault(m => m.Name == meetingRoom.Name) == null)
                    {
                        db.MeetingRooms.Add(meetingRoom);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "A meeting room with the same name already exists.");
                    }
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
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
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roomToUpdate = db.MeetingRooms.Find(id);
            if (TryUpdateModel(roomToUpdate, "",
               new string[] { "Name", "Size", "Location", "Equipment" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again, and if the problem persists, contact your system administrator.");
                }
            }
            return View(roomToUpdate);
        }

        // GET: MeetingRooms/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Please try again, and if the problem persists, contact your system administrator.";
            }
            MeetingRoom meetingRoom = db.MeetingRooms.Find(id);
            if (meetingRoom == null)
            {
                return HttpNotFound();
            }
            return View(meetingRoom);
        }

        // POST: MeetingRooms/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                MeetingRoom meetingRoomToDelete = db.MeetingRooms.Find(id);
                //check if any meeting is happening in the room at a future date
                Booking bookingUsingRoomInFuture = db.Bookings.FirstOrDefault(x => (x.RoomId == id) && (x.Date >= DateTime.Today));
                if (bookingUsingRoomInFuture == null)
                {
                    db.MeetingRooms.Remove(meetingRoomToDelete);
                    db.SaveChanges();
                }
                else
                {
                    TempData["msg"] = "<script>alert('This meeting room cannot be deleted as it has been booked for future meetings.');</script>";
                }

            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
