using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.ViewModels
{
    public class TypeRequestVM
    {
        public int Id { get; set; }

        [Display(Name = "Тип заявки")]
        [Required(ErrorMessage = "Введите название типа заявки!")]
        public string Name { get; set; }
    }
}
