using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.Entity
{
    public class Years
    {
        [Key]
        public int Id { get; set; }
        public string Year { get; set; }
        public bool CalendarInputStatus { get; set; }
    }
}
