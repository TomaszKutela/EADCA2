using CA2_IMeet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CA2_IMeet.Tests.Controllers
{
    [TestClass]
    public class MeetingRoomsControllerTests

    {
        private static List<MeetingRoom> rooms = new List<MeetingRoom>()
        {
            new MeetingRoom {Name ="North Wall", Size = 20, Location = "One Spencer Dock", Equipment="Wi-Fi Projector VC WB"},
            new MeetingRoom {Name ="Royal Canal", Size = 18, Location ="One Spencer Dock", Equipment="Wi-Fi Projector VC WB"},
            new MeetingRoom {Name ="Grand Canal", Size = 8, Location ="One Spencer Dock", Equipment="Wi-Fi VC WB"},
            new MeetingRoom {Name ="River Liffey", Size = 4, Location ="One Spencer Dock", Equipment="Wi-Fi Conf Phone"}
       };



        [TestMethod()]
        public void IndexTest(string sortOrder, string searchString)
        {/*
            // Arrange
            var meetroomController = new MeetingRoomsController();

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
            // Action
            var result = meetroomController.Index();
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.Fail();
        }

        [TestMethod()]
        public void DetailsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest1()
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
        public void DeleteTest1()
        {
            Assert.Fail();
        }
        /* Draft Meeting Room Contoller Tests
[TestMethod]
public void Index(string sortOrder, string searchString)
{
// Arrange
MeetingRoomsController meetroomController = new MeetingRoomsController();

// Act


// Assert

}

[TestMethod]
public void Details(string keyword)
{
// Arrange
MeetingRoomsController meetroomController = new MeetingRoomsController();

// Act


// Assert
}
[TestMethod]
public void Create()
{
// Arrange
MeetingRoomsController meetroomController = new MeetingRoomsController();

// Act


// Assert
}


[TestMethod]
public void Create([Bind(Include = "Name,Size,Location,Equipment")] Models.MeetingRoom meetingRoom)
{

// Arrange
MeetingRoomsController meetroomController = new MeetingRoomsController();

// Act


// Assert
}

[TestMethod]
public void Edit(int? id)
{
// Arrange
MeetingRoomsController meetroomController = new MeetingRoomsController();

// Act


// Assert
}

[TestMethod]
public void EditPost(int? id)
{
// Arrange
MeetingRoomsController meetroomController = new MeetingRoomsController();

// Act


// Assert
}

[TestMethod]
public void Delete(int? id, bool? saveChangesError = false)
{
// Arrange
MeetingRoomsController meetroomController = new MeetingRoomsController();

// Act


// Assert
}

[TestMethod]
public void Delete(int id)
{
// Arrange
MeetingRoomsController meetroomController = new MeetingRoomsController();

// Act


// Assert
}

*/
        }
    }
}


