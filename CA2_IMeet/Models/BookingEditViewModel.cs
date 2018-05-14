using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CA2_IMeet.Models
{
    public class BookingEditViewModel : BookingCreateViewModel
    {
        [Required(ErrorMessage = "Indicate Meeting Reference.")]
        [StringLength(55, ErrorMessage = "Meeting Title cannot be longer than 55 characters.")]
        [Display(Name = "Meeting Title")]
        public string MeetingReference { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }
    }
}