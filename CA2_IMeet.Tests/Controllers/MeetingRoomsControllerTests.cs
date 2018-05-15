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
        public void CreateRoomTest()
        {
            //Arrange
            TestBookingContext tbc = new TestBookingContext();
            MeetingRoomsController controller = new MeetingRoomsController(tbc);
            MeetingRoom roomToCreate = new MeetingRoom() { Name = "Interview Room", Size = 7, Location = "Second Floor" };

            // Act
            var result = controller.Create(roomToCreate) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void CreateRoom_ShouldFailIfNameAlreadyTaken()
        {
            //Arrange
            TestBookingContext tbc = new TestBookingContext();
            MeetingRoomsController controller = new MeetingRoomsController(tbc);
            MeetingRoom roomToAdd = new MeetingRoom() { Name = "Board Room", Size = 10, Location = "Second Floor" };
            tbc.MeetingRooms.Add(roomToAdd);

            MeetingRoom roomToCreate = new MeetingRoom() { Name = "Board Room", Size = 7, Location = "First Floor" };
            var result = controller.Create(roomToCreate) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.ModelState[""].Errors);
        }

        [TestMethod()]
        public void IndexTestNameLike()
        {
            //Arrange
            TestBookingContext tbc = new TestBookingContext();
            MeetingRoomsController controller = new MeetingRoomsController(tbc);
            MeetingRoom room1 = new MeetingRoom() { Name = "Chat Room", Size = 4, Location = "Ground Floor" };
            MeetingRoom room2 = new MeetingRoom() { Name = "Board Room", Size = 8, Location = "First Floor" };
            tbc.MeetingRooms.Add(room1);
            tbc.MeetingRooms.Add(room2);

            // Act
            ViewResult result = controller.Index("", "hat") as ViewResult;

            // Assert
            var rooms = (List<MeetingRoom>)result.ViewData.Model;
            foreach (var r in rooms)
            {
                StringAssert.Contains(r.Name, "hat");
            }
            Assert.AreEqual(1, rooms.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            TestBookingContext tbc = new TestBookingContext();
            MeetingRoomsController controller = new MeetingRoomsController(tbc);
            MeetingRoom roomToAdd = new MeetingRoom() { Name = "Interview Room", Size = 7, Location = "Second Floor" };
            tbc.MeetingRooms.Add(roomToAdd);

            ViewResult result = controller.Details("Interview Room") as ViewResult;

            var boardRoom = (MeetingRoom)result.ViewData.Model;
            Assert.AreEqual(boardRoom.Name, "Interview Room");
            Assert.AreEqual(boardRoom.Size, 7);
        }
    }
}



