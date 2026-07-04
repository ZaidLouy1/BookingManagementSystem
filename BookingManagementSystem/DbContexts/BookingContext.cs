using BookingManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.DbContexts
{
    public class BookingContext:DbContext
    {

        public BookingContext(DbContextOptions<BookingContext> options): base(options)
        {
            
        }

        // Tables 
        public DbSet<Booking> bookings { get; set; }
        public DbSet<AuditLog> auditlogs { get; set; }


    }
}
