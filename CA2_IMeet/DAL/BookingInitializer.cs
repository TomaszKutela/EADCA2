using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA2_IMeet.DAL
{
    public class BookingInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BookingContext>
    {
        protected override void Seed(BookingContext context)
        {
        }
    }
}