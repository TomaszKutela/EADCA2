using CA2_IMeet.Controllers;
using CA2_IMeet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace CA2_IMeet.Tests.Controllers
{
    [TestClass]
    public class BookingControllerTests
    {
        [TestMethod]
        public void Delete_ShouldFail_WhenNullID()
        {
            var controller = new BookingController();
            var expected = (int)System.Net.HttpStatusCode.BadRequest;

            var badresult = controller.Delete(null) as HttpStatusCodeResult;

            Assert.AreEqual(expected, badresult.StatusCode);
        }



        [TestMethod]
        public void Delete_ShouldFail_WhenIncorrectID()
        {
            var controller = new BookingController();

            var badresult = controller.Delete(99999);
            Assert.IsInstanceOfType(badresult, typeof(HttpNotFoundResult));
        }

        [TestMethod()]
        public void CreateTest_ShouldFailIfInvalidTimes()
        {
            BookingController controller = new BookingController();
            BookingCreateViewModel bookingToAdd = new BookingCreateViewModel() { Date = new DateTime(2018, 11, 29), End_DateTime = new DateTime(2018, 11, 29, 10, 0, 0), Start_DateTime = new DateTime(2018, 11, 29, 11, 0, 1) };

            var result = controller.CreateStep2(bookingToAdd) as ViewResult;

            Assert.IsTrue(!controller.ModelState.IsValid);
            Assert.IsTrue(controller.ViewData.ModelState.Count == 1,
                 "Please check start and end times. A meeting cannot end before it starts.");
        }
    }
}
