using HRMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.DbContexts
{
    public class HRMSContext:DbContext
    {

        public HRMSContext(DbContextOptions<HRMSContext> options): base(options)
        {
            
        }

        // Tables 
        public DbSet<Booking> bookings { get; set; }
        public DbSet<AuditLog> auditlogs { get; set; }


    }
}
