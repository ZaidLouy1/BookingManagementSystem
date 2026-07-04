using System.ComponentModel.DataAnnotations;

namespace BookingManagementSystem.Models
{
    public class AuditLog
    {
        public long Id { get; set; }

        public long BookingId { get; set; }

        [MaxLength(50)]
        public string Action { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}