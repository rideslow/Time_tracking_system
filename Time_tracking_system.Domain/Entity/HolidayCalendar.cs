using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_tracking_system.Domain.Entity
{
    public class HolidayCalendar
    {
        [Key]
        public int Id { get; set; }
        public DateTime HolidayDate { get; set; }
        
        [ForeignKey("Years_id")]
        public Years Years { get; set; }
        public int Years_id { get; set; }
    }

}
