namespace HRMS.Dtos.Booking
{
    public class SearchBookingDto
    {
        public string ResourceName { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}
