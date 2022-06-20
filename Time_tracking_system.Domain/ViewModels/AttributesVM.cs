using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.ViewModels
{
    public class AttributesVM
    {
        public int Id { get; set; }

        [Display(Name = "Атрибут")]
        [Required(ErrorMessage = "Введите название атрибута заявки!")]
        public string NameAttributes { get; set; }

        [Display(Name = "Тип данных")]
        [Required]
        public string TypeData { get; set; }
    }
}
