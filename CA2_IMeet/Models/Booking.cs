using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CA2_IMeet.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Indicate Meeting Reference.")]
        [StringLength(55, ErrorMessage = "Meeting Title cannot be longer than 55 characters.")]
        [Display(Name = "Meeting Title")]
        public string MeetingReference { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual MeetingRoom MeetingRoom { get; set; }

        [Required(ErrorMessage = "Indicate meeting date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Indicate when meeting starts.")]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime Start_DateTime { get; set; }

        [Required(ErrorMessage = "Indicate when meeting ends.")]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime End_DateTime { get; set; }

        [Display(Name = "Meeting Owner")]
        public string UserId { get; set; }

        //method to check if room is not already booked on another booking
        public bool IsValidBooking(Booking newBooking)
        {
            if (newBooking.RoomId == RoomId && newBooking.Date == Date)
            {
                if((newBooking.Start_DateTime > End_DateTime) || (newBooking.End_DateTime < Start_DateTime))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}