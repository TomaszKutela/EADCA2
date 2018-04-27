using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CA2_IMeet.Models
{
    public class MeetingRoom
    {
        [Key]
        public int RoomId { get; set; }

        [Required(ErrorMessage ="Name cannot be left blank.")]
        [Display(Name = "Room Name")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Indicate Room Capacity.")]
        [Range(1,100)]
        public int Size { get; set; }

        [Required(ErrorMessage = "Indicate Room Location.")]
        [StringLength(30, ErrorMessage = "Location cannot be longer than 30 characters.")]
        public string Location { get; set; }

        public string Equipment { get; set; }

    }
}