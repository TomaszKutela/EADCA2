using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CA2_IMeet.Models
{
    public class BookingViewModel
    {
        private DateTime start_DateTime;
        private DateTime end_DateTime;

        [HiddenInput]
        public int? BookingId { get; set; }

        [Required(ErrorMessage = "Indicate Meeting Reference.")]
        [StringLength(55, ErrorMessage = "Meeting Title cannot be longer than 55 characters.")]
        [Display(Name = "Meeting Title")]
        public string MeetingReference { get; set; }

        [Required(ErrorMessage = "Indicate meeting date.")]
        [DataType(DataType.Date)]
        [Weekdays]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Indicate when meeting starts.")]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime Start_DateTime
        {
            get
            {
                return start_DateTime;
            }
            set
            {
                start_DateTime = new DateTime(Date.Year, Date.Month, Date.Day, value.Hour, 0, 1);
            }
        }

        [Required(ErrorMessage = "Indicate when meeting ends.")]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime End_DateTime
        {
            get
            {
                return end_DateTime;
            }
            set
            {
                end_DateTime = new DateTime(Date.Year, Date.Month, Date.Day, value.Hour, 0, 0);
            }
        }

        public bool InvalidStartAndEnd()
        {
            if (Start_DateTime >= End_DateTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}