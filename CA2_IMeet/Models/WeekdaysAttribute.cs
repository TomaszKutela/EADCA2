using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CA2_IMeet.Models
{
    public class WeekdaysAttribute : ValidationAttribute
    {
        public WeekdaysAttribute()
        {
            this.ErrorMessage = "Meetings cannot happen at the weekend. Please pick another date.";
        }

        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            bool result = true;
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                result = false;
            }
            return result;
        }
    }
}