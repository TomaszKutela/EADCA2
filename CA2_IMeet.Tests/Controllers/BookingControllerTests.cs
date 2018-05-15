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
        //[TestMethod]
        //public void PutProduct_ShouldFail_WhenDifferentID()
        //{
        //    var controller = new ProductController(new TestStoreAppContext());

        //    var badresult = controller.PutProduct(999, GetDemoProduct());
        //    Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        //}

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
