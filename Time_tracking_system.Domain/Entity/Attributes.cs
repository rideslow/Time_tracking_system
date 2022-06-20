using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.Entity
{
    //Атрибуты заявок
    public class Attributes
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Атрибут")]
        public string NameAttributes { get; set; }

        [Display(Name = "Тип данных")]
        public string TypeData { get; set; }

    }
}
