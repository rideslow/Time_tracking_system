using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<Attributes_TypeRequest> Attributes_TypesRequests { get; set; }        
        public DbSet<Request> Requests { get; set; }
        public DbSet<StatusRequest> StatusRequests { get; set; }
        public DbSet<TypeRequest> TypesRequests { get; set; }
        public DbSet<TypeVocation> TypesVocations { get; set; }
        public DbSet<ValueRequestAttributes> ValuesRequestsAttributes { get; set; }
        public DbSet<HolidayCalendar> HolidaysCalendar { get; set; }
        public DbSet<Years> Years { get; set; }
    }
}
