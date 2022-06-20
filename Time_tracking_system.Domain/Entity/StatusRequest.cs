using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.Entity
{
    //Статус заявки
    public class StatusRequest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
