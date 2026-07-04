using BookingManagementSystem.DbContexts;
using BookingManagementSystem.Dtos.Booking;
using BookingManagementSystem.Dtos.Bookings;
using BookingManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {



        private readonly BookingContext _dbContext;

        public BookingsController(BookingContext dbContext)
        {
            _dbContext = dbContext;
        }












        [HttpPost]
        public IActionResult Add([FromBody] SaveBookingDto newBooking)
        {
            if (newBooking.StartDateTime >= newBooking.EndDateTime)
            {
                return BadRequest("StartDateTime must be before EndDateTime.");
            }

            var hasOverlap = _dbContext.bookings.Any(x =>
                x.ResourceName == newBooking.ResourceName &&
                x.Status == "Active" &&
                newBooking.StartDateTime < x.EndDateTime &&
                newBooking.EndDateTime > x.StartDateTime
            );

            if (hasOverlap)
            {
                return BadRequest("This resource is already booked during the selected time.");
            }

            var booking = new Booking()
            {
                ResourceName = newBooking.ResourceName,
                UserName = newBooking.UserName,
                StartDateTime = newBooking.StartDateTime,
                EndDateTime = newBooking.EndDateTime,
                Status = "Active",
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.bookings.Add(booking);
            _dbContext.SaveChanges();

            var auditLog = new AuditLog()
            {
                BookingId = booking.Id,
                UserName = booking.UserName,
                Action = "Created",
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.auditlogs.Add(auditLog);
            _dbContext.SaveChanges();

            return Ok(booking.Id);
        }













        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var data = _dbContext.bookings
                .Select(x => new BookingDto
                {
                    Id = x.Id,
                    ResourceName= x.ResourceName,
                    UserName = x.UserName,
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CancelledAt = x.CancelledAt
                })
                .FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return NotFound("Booking Not Found");
            }

            return Ok(data);
        }
















        [HttpGet("GetByCriteria")]
        public IActionResult GetByCriteria([FromQuery] SearchBookingDto searchBookingDto)
        {
            var data = _dbContext.bookings
                .Where(x =>
                    (searchBookingDto.ResourceName == null || x.ResourceName == searchBookingDto.ResourceName) &&
                    (searchBookingDto.From == null || x.StartDateTime >= searchBookingDto.From) &&
                    (searchBookingDto.To == null || x.EndDateTime <= searchBookingDto.To)
                )
                .OrderBy(x => x.StartDateTime)
                .Select(x => new BookingDto
                {
                    Id = x.Id,
                    ResourceName = x.ResourceName,
                    UserName = x.UserName,
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CancelledAt = x.CancelledAt
                });

            return Ok(data);
        }














        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var booking = _dbContext.bookings.FirstOrDefault(x => x.Id == id);

            if (booking == null)
            {
                return NotFound("Booking Not Found");
            }

            if (booking.Status == "Cancelled")
            {
                return BadRequest("Booking is already cancelled.");
            }

            booking.Status = "Cancelled";
            booking.CancelledAt = DateTime.UtcNow;

            var auditLog = new AuditLog()
            {
                BookingId = booking.Id,
                UserName = booking.UserName,
                Action = "Cancelled",
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.auditlogs.Add(auditLog);
            _dbContext.SaveChanges();

            return Ok();
        }














        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var data = _dbContext.bookings
                .OrderByDescending(x => x.Id)
                .Select(x => new BookingDto
                {
                    Id = x.Id,
                    ResourceName = x.ResourceName,
                    UserName = x.UserName,
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CancelledAt = x.CancelledAt
                });

            return Ok(data);
        }








        [HttpGet("AuditLogs")]
          public IActionResult GetAuditLogs()
        {
           var data = _dbContext.auditlogs
          .OrderByDescending(x => x.CreatedAt)
           .Select(x => new
          {
            x.Id,
            x.BookingId,
            x.UserName,
            x.Action,
           
        });

    return Ok(data);
}




    }
    }
