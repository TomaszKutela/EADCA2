using CA2_IMeet.Models;
using System;
using System.Linq;

namespace CA2_IMeet.Tests
{
    class TestMeetingRoomDbSet : TestDbSet<MeetingRoom>
    {
        public override MeetingRoom Find(params object[] keyValues)
        {
            return this.SingleOrDefault(room => room.RoomId == (int)keyValues.Single());
        }
    }

    class TestBookingDbSet : TestDbSet<Booking>
    {
        public override Booking Find(params object[] keyValues)
        {
            return this.SingleOrDefault(b => b.BookingId == (int)keyValues.Single());
        }
    }
}
