using CA2_IMeet.DAL;
using CA2_IMeet.Models;
using System;
using System.Data.Entity;

namespace CA2_IMeet.Tests
{
    class TestBookingContext : IBookingContext
    {
        public TestBookingContext()
        {
            this.MeetingRooms = new TestMeetingRoomDbSet();
            this.Bookings = new TestBookingDbSet();
        }

        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Object item) { }
        public void Dispose() { }
    }
}
