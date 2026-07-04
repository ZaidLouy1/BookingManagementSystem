using BookingManagementSystem.Controllers;
using BookingManagementSystem.DbContexts;
using BookingManagementSystem.Dtos.Bookings;
using BookingManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookingManagementSystem.Tests
{
    public class BookingTests
    {
        private BookingContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new BookingContext(options);
        }









        [Fact]
        public void AddBooking_Should_Create_Booking_When_Data_Is_Valid()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new BookingsController(dbContext);

            var newBooking = new SaveBookingDto
            {
                ResourceName = "Meeting Room 1",
                UserName = "Zaid",
                StartDateTime = new DateTime(2026, 7, 5, 10, 0, 0),
                EndDateTime = new DateTime(2026, 7, 5, 11, 0, 0)
            };

            // Act
            var result = controller.Add(newBooking);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1L, okResult.Value);

            Assert.Single(dbContext.bookings);
            Assert.Single(dbContext.auditlogs);
        }












        [Fact]
        public void AddBooking_Should_ReturnBadRequest_When_StartDate_Is_After_EndDate()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new BookingsController(dbContext);

            var newBooking = new SaveBookingDto
            {
                ResourceName = "Meeting Room 1",
                UserName = "Zaid",
                StartDateTime = new DateTime(2026, 7, 5, 11, 0, 0),
                EndDateTime = new DateTime(2026, 7, 5, 10, 0, 0)
            };

            // Act
            var result = controller.Add(newBooking);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Empty(dbContext.bookings);
            Assert.Empty(dbContext.auditlogs);
        }















        [Fact]
        public void AddBooking_Should_ReturnBadRequest_When_Booking_Overlaps()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new BookingsController(dbContext);

            dbContext.bookings.Add(new Booking
            {
                ResourceName = "Meeting Room 1",
                UserName = "Ahmed",
                StartDateTime = new DateTime(2026, 7, 5, 10, 0, 0),
                EndDateTime = new DateTime(2026, 7, 5, 11, 0, 0),
                Status = "Active"
            });

            dbContext.SaveChanges();

            var newBooking = new SaveBookingDto
            {
                ResourceName = "Meeting Room 1",
                UserName = "Zaid",
                StartDateTime = new DateTime(2026, 7, 5, 10, 30, 0),
                EndDateTime = new DateTime(2026, 7, 5, 11, 30, 0)
            };

            // Act
            var result = controller.Add(newBooking);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Single(dbContext.bookings);
            Assert.Empty(dbContext.auditlogs);
        }















        [Fact]
        public void DeleteBooking_Should_Cancel_Booking()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new BookingsController(dbContext);

            var booking = new Booking
            {
                ResourceName = "Meeting Room 1",
                UserName = "Zaid",
                StartDateTime = new DateTime(2026, 7, 5, 10, 0, 0),
                EndDateTime = new DateTime(2026, 7, 5, 11, 0, 0),
                Status = "Active"
            };

            dbContext.bookings.Add(booking);
            dbContext.SaveChanges();

            // Act
            var result = controller.Delete(booking.Id);

            // Assert
            Assert.IsType<OkResult>(result);

            var updatedBooking = dbContext.bookings.First();

            Assert.Equal("Cancelled", updatedBooking.Status);
            Assert.NotNull(updatedBooking.CancelledAt);

            Assert.Single(dbContext.auditlogs);

            var auditLog = dbContext.auditlogs.First();
            Assert.Equal("Cancelled", auditLog.Action);
        }














        [Fact]
        public void GetBookingById_Should_Return_NotFound_When_Booking_Does_Not_Exist()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var controller = new BookingsController(dbContext);

            // Act
            var result = controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }


    }
    }