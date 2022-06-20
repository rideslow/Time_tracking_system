using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.Entity
{
    //Тип отпуска
    public class TypeVocation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
