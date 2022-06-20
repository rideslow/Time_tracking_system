using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.Entity
{
    //Тип заявки
    public class TypeRequest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
