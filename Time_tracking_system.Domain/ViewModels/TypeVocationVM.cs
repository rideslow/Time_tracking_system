using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.ViewModels
{
    public class TypeVocationVM
    {
        public int Id { get; set; }

        [Display(Name = "Тип отпуска")]
        [Required(ErrorMessage ="Введите название типа отпуска!")]
        public string Name { get; set; }
    }

    public class CreateRequestTypeVocationVM
    {
        public int index;
        public List<TypeVocationVM> typeVocations { get; set; }
    }
}
