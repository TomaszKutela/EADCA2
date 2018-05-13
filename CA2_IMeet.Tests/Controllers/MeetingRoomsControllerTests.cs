using CA2_IMeet.Controllers;
using CA2_IMeet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CA2_IMeet.Tests.Controllers
{
    [TestClass]
    public class MeetingRoomsControllerTests

    {
        [TestMethod()]
        public void IndexTestNameLikeChat()
        {
            MeetingRoomsController controller = new MeetingRoomsController();

            ViewResult result = controller.Index("", "chat") as ViewResult;

            // Assert
            var rooms = (List<MeetingRoom>)result.ViewData.Model;
            foreach (var r in rooms)
            {
                StringAssert.Contains(r.Name, "Chat");
            }
        }

        [TestMethod()]
        public void DetailsTestForInterviewRoom()
        {
            MeetingRoomsController controller = new MeetingRoomsController();

            ViewResult result = controller.Details("Interview Room") as ViewResult;

            var boardRoom = (MeetingRoom)result.ViewData.Model;
            Assert.AreEqual(boardRoom.Name, "Interview Room");
            Assert.AreEqual(boardRoom.Size, 7);
        }

        [TestMethod()]
        public void CreateTest()
        {
            //Arrange
            MeetingRoomsController controller = new MeetingRoomsController();
            MeetingRoom roomToCreate = new MeetingRoom() { Name = "Interview Room", Size = 7, Location = "Second Floor" };

            // Act
            var result = controller.Create(roomToCreate) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
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
        public void DeleteTest1()
        {
            Assert.Fail();
        }
    }
}



