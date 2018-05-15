using CA2_IMeet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace CA2_IMeet.DAL
{
    public interface IBookingContext : IDisposable
    {
        DbSet<MeetingRoom> MeetingRooms { get; }
        DbSet<Booking> Bookings { get; }

        int SaveChanges();
        void MarkAsModified(Object item);
    }
}