using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CA2_IMeet.Controllers;
using CA2_IMeet.DAL;
using CA2_IMeet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CA2_IMeet.Tests.Controllers
{
    [TestClass]
    public class BookingControllerTests 
    {
        [TestMethod()]
        public void IndexTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateStep2Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreatePostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditPostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAvailableRoomsTest()
        {
            Assert.Fail();
        }
        /*

[TestMethod]
public void Index()
{
// Arrange
BookingController bookingController = new BookingController();


// Act
{
IEnumerable<Booking> bookingsToShow;
if (User.IsInRole("Admin"))
{
bookingsToShow = db.Bookings.Where(b => b.Date >= DateTime.Today).Include(b => b.MeetingRoom).OrderBy(b => b.Date).ThenBy(b => b.Start_DateTime).ToList();
}
else
{
string user_id = User.Identity.GetUserName();
bookingsToShow = db.Bookings.Where(b => b.UserId == user_id).Where(b => b.Date >= DateTime.Today).Include(b => b.MeetingRoom).OrderBy(b => b.Date).ThenBy(b => b.Start_DateTime).ToList();
}
// Assert

return View(bookingsToShow);

}

/*          // GET: Booking/Create
//Used to Collect Criteria to Find Room
public ActionResult Create()
{
PopulateStartTimeDropDownList();
PopulateEndTimeDropDownList();
return View();
}

[TestMethod]
public void Create()
{
// Arrange
BookingController bookingController = new BookingController();

// Act
PopulateStartTimeDropDownList();
PopulateEndTimeDropDownList();

// Assert
return View();

}
/*

[TestMethod]
public void Create([Bind(Include = "MeetingReference, RoomId, Date, Start_DateTime, End_DateTime")] Booking booking)
{
// Arrange
BookingController bookingController = new BookingController();

// Act


// Assert

}
[TestMethod]
public void Edit(int? id)
{
// Arrange
BookingController bookingController = new BookingController();

// Act


// Assert

}
[TestMethod]
public void EditPost(int? id)
{
// Arrange
BookingController bookingController = new BookingController();

// Act


// Assert

}
[TestMethod]
public void Delete(int? id, bool? saveChangesError = false)
{
// Arrange
BookingController bookingController = new BookingController();

// Act


// Assert

}
[TestMethod]
public void DeleteConfirmed(int id)
{
// Arrange
BookingController bookingController = new BookingController();

// Act


// Assert

}
*/

    }
}
