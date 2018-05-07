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
using Microsoft.AspNet.Identity;

namespace CA2_IMeet.Controllers
{
    //[Authorize]
    public class BookingController : Controller
    {
        private BookingContext db = new BookingContext();

        // GET: Booking/Index
        public ActionResult Index()
        {
            IEnumerable<Booking> bookingsToShow;
            //if (User.IsInRole("Admin"))
            //{
                bookingsToShow = db.Bookings.Where(b => b.Date >= DateTime.Now).Include(b => b.MeetingRoom).OrderBy(b => b.Date).ThenBy(b => b.Start_DateTime);
            //}
            //else
            //{
            //string user_id = User.Identity.GetUserId();
            //bookingsToShow = db.Bookings.Where(b => b.UserId == user_id && b.Date >= DateTime.Now).Include(b => b.MeetingRoom).OrderBy(b => b.Date).ThenBy(b => b.Start_DateTime);
            //}

            return View(bookingsToShow.ToList());
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
            PopulateStartTimeDropDownList();
            PopulateEndTimeDropDownList();
            return View();
        }

        // GET: Booking/Create
        public ActionResult CreateStep2(BookingViewModel mv)
        {
            //if ModelState.IsValid !!! CHECK!! sinon retourne a l'ecran precedent!

            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name");
            PopulateStartTimeDropDownList(mv.Start_DateTime);
            PopulateEndTimeDropDownList(mv.End_DateTime);
            return View(mv);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingReference, RoomId, Date, Start_DateTime, End_DateTime")] Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //check that the picked date is not a weekend
                    if (booking.Date.DayOfWeek == DayOfWeek.Saturday || booking.Date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        ModelState.AddModelError("", "Meetings cannot happen at the weekend. Please pick another date.");
                        ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", booking.RoomId);
                        PopulateStartTimeDropDownList(booking.Start_DateTime);
                        PopulateEndTimeDropDownList(booking.End_DateTime);
                        return View(booking);
                    }
                    
                    //change end and start date to match picked date
                    booking.Start_DateTime = new DateTime(booking.Date.Year, booking.Date.Month, booking.Date.Day, booking.Start_DateTime.Hour, 0, 1);
                    booking.End_DateTime = new DateTime(booking.Date.Year, booking.Date.Month, booking.Date.Day, booking.End_DateTime.Hour, 0, 0);

                    //check that end time is after start time and that start time and end time are different
                    if (booking.Start_DateTime >= booking.End_DateTime)
                    {
                        ModelState.AddModelError("", "Please check start and end times. A meeting cannot end before it starts.");
                        ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", booking.RoomId);
                        PopulateStartTimeDropDownList(booking.Start_DateTime);
                        PopulateEndTimeDropDownList(booking.End_DateTime);
                        return View(booking);
                    }

                    //make sure room is still free
                    foreach (Booking b in db.Bookings)
                    {
                        if (!b.IsValidBooking(booking))
                        {
                            ModelState.AddModelError("", "This room is not available any longer for booking. Please try to make another booking.");
                            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", booking.RoomId);
                            PopulateStartTimeDropDownList(booking.Start_DateTime);
                            PopulateEndTimeDropDownList(booking.End_DateTime);
                            return View(booking);
                        }
                    }
                    //assign user ID!! TO DO

                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again, and if the problem persists, contact your system admistrator");
            }
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", booking.RoomId);
            PopulateStartTimeDropDownList(booking.Start_DateTime);
            PopulateEndTimeDropDownList(booking.End_DateTime);
            return View(booking);
        }


        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
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
            //check if the booking is from the user logged in ***unless he is an admin***
            //try
            //{
            //    if (!User.IsInRole("Admin") && booking.UserId != User.Identity.GetUserName())
            //    {
            //        throw new UnauthorizedAccessException("Oops, this booking doesn't seem to be yours, you cannot edit it.");
            //    }
            //using a viewmodel to pass on data from controller to view then to controller again when posting
            BookingViewModel vm = new BookingViewModel
            {
                BookingId = booking.BookingId,
                Date = booking.Date,
                MeetingReference = booking.MeetingReference,
                Start_DateTime = booking.Start_DateTime,
                End_DateTime = booking.End_DateTime
            };

            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", booking.RoomId);
            PopulateStartTimeDropDownList(booking.Start_DateTime);
            PopulateEndTimeDropDownList(booking.End_DateTime);
            return View(vm);
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    return View("NotAuthorizedError", new HandleErrorInfo(ex, "Booking", "Edit"));
            //}
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(BookingViewModel vm)
        {
            if (vm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking bookingToUpdate = db.Bookings.Find(vm.BookingId.Value);

            //check valid times
            if (vm.InvalidStartAndEnd())
            {
                ModelState.AddModelError("", "Please check start and end times. A meeting cannot end before it starts.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //assign vm values to booking
                    bookingToUpdate.MeetingReference = vm.MeetingReference;
                    bookingToUpdate.Date = vm.Date;
                    bookingToUpdate.Start_DateTime = vm.Start_DateTime;
                    bookingToUpdate.End_DateTime = vm.End_DateTime;

                    List<Booking> checkSet = db.Bookings.ToList();
                    checkSet.Remove(bookingToUpdate);
                    //make sure room is free
                    foreach (Booking b in checkSet)
                    {
                        if (!b.IsValidBooking(bookingToUpdate))
                        {
                            ModelState.AddModelError("", "This room is not available any longer for booking. Please try to make another booking.");
                        }
                        else
                        {
                            try
                            {
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            catch (RetryLimitExceededException)
                            {
                                ModelState.AddModelError("", "Unable to save changes. Please try again. If problem persists, contact your system administrator.");
                            }
                        }
                    }                 
                }
            }
            
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Name", bookingToUpdate.RoomId);
            PopulateStartTimeDropDownList(vm.Start_DateTime);
            PopulateEndTimeDropDownList(vm.End_DateTime);
            return View(vm);
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
            //check if the booking is from the user logged in ***unless he is an admin***
            //try
            //{
            //    if (!User.IsInRole("Admin") && booking.UserId != User.Identity.GetUserName())
            //    {
            //        throw new UnauthorizedAccessException("Oops, this booking doesn't seem to be yours, you cannot delete it.");
            //    }
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    return View("NotAuthorizedError", new HandleErrorInfo(ex, "Booking", "Index"));
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Find Available Rooms when date and time are picked
        private void FindAvailableRooms(DateTime date, DateTime startTime, DateTime endTime)
        {
            List<MeetingRoom> availableRooms = db.MeetingRooms.ToList();
            IEnumerable<Booking> filteredBookings = db.Bookings.Where(b => b.Date == date).ToList();
            foreach (MeetingRoom r in db.MeetingRooms)
            {
                filteredBookings = filteredBookings.Where(b => b.RoomId == r.RoomId).ToList();
                if (filteredBookings.Any(b => (!((b.Start_DateTime > endTime) || (b.End_DateTime < startTime)))))
                {
                    availableRooms.Remove(r);
                }
            }
            ViewBag.AvailableRooms = availableRooms;
        }

        //populate start time dropdown list
        private void PopulateStartTimeDropDownList(DateTime? selectedStartTime = null)
        {
            List<SelectListItem> startTimes = new List<SelectListItem>();
            DateTime date = DateTime.MinValue.AddHours(8); // start at 8am
            DateTime endDate = DateTime.MinValue.AddHours(17); // end at 5pm
            while (date < endDate)
            {
                //used for edits to pass on the time that was previously selected
                bool selected = false;
                if (selectedStartTime.HasValue)
                {
                    if (selectedStartTime.Value.Hour == date.Hour)
                    {
                        selected = true;
                    }
                }

                startTimes.Add(new SelectListItem { Text = date.ToShortTimeString(), Value = date.ToString(), Selected = selected });
                date = date.AddHours(1);
            }
            ViewBag.Start_DateTime = startTimes;
        }

        //populate end time dropdown list
        private void PopulateEndTimeDropDownList(DateTime? selectedEndTime = null)
        {
            List<SelectListItem> endTimes = new List<SelectListItem>();
            DateTime date = DateTime.MinValue.AddHours(9); // start at 8am
            DateTime endDate = DateTime.MinValue.AddHours(17); // end at 5pm
            while (date <= endDate)
            {
                //used for edits to pass on the time that was previously selected
                bool selected = false;
                if (selectedEndTime.HasValue)
                {
                    if (selectedEndTime.Value.Hour == date.Hour)
                    {
                        selected = true;
                    }
                }
                endTimes.Add(new SelectListItem { Text = date.ToShortTimeString(), Value = date.ToString(), Selected = selected });
                date = date.AddHours(1);
            }
            ViewBag.End_DateTime = endTimes;
        }
    }
}
