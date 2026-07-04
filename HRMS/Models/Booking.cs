using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Booking
    {
        public long Id { get; set; }

        [MaxLength(50)]
        public string ResourceName { get; set; } // meeting room 1 , meeting room 2

        [MaxLength(50)]
        public string UserName { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CancelledAt { get; set; }
    }
}
