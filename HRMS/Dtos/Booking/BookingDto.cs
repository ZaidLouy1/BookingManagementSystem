namespace HRMS.Dtos.Bookings
{
    public class BookingDto
    {
        public long Id { get; set; }

        public string ResourceName { get; set; }

        public string UserName { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Status { get; set; }


        public DateTime CreatedAt { get; set; }

        public DateTime? CancelledAt { get; set; }
    }
}