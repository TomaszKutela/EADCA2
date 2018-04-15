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
        public int Booking_ID { get; set; }

        [Required(ErrorMessage = "Indicate Meeting Reference.")]
        public string MeetingReference { get; set; }

        [Required]
        [ForeignKey("MeetingRoom")]
        public int Room_ID { get; set; }

        [Required(ErrorMessage = "Indicate when meeting starts.")]
        [Display(Name = "Start Time")]
        public DateTime Start_DateTime { get; set; }

        [Required(ErrorMessage = "Indicate when meeting ends.")]
        [Display(Name = "End Time")]
        public DateTime End_DateTime { get; set; }

        [ForeignKey("IdentityModel")]
        public string Username { get; set; }
    }
}