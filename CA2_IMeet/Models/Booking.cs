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
        public string MeetingReference { get; set; }

        [Required]
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual MeetingRoom MeetingRoom { get; set; }

        [Required(ErrorMessage = "Indicate meeting date.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Indicate when meeting starts.")]
        [Display(Name = "Start Time")]
        public DateTime Start_DateTime { get; set; }

        [Required(ErrorMessage = "Indicate when meeting ends.")]
        [Display(Name = "End Time")]
        public DateTime End_DateTime { get; set; }

        public string UserId { get; set; }

    }
}