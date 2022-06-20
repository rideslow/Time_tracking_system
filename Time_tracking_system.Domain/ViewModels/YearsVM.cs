using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.ViewModels
{
    public class YearsVM
    {
        public int Id { get; set; }
        [Display(Name = "Год")]
        public string Year { get; set; }

        [Display(Name = "Статус ввода календаря в обращение")]
        public bool CalendarInputStatus { get; set; }
    }
}
