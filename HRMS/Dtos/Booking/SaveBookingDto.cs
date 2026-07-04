namespace HRMS.Dtos.Bookings
{
    public class SaveBookingDto
    {
        public string ResourceName { get; set; }

        public string UserName { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
