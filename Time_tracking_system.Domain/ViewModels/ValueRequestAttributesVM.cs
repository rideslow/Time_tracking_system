using System;
using System.ComponentModel.DataAnnotations;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.Domain.ViewModels
{
    public class ValueRequestAttributesVM
    {
        public int Id { get; set; }

        [Display(Name = "Заявка")]
        public Request Request { get; set; }
        public int? Request_id { get; set; }

        [Display(Name = "Атрибут")]
        public Attributes_TypeRequest Attributes_TypeRequest { get; set; }
        public int? Attr_TypeRequest_id { get; set; }

        [Display(Name = "Сотрудник")]
        public Employee Employee { get; set; }
        public string ValueEmloyee_id { get; set; }

        [Display(Name = "Тип отпуска")]
        public TypeVocation TypeVocation { get; set; }
        public int? ValueTypeVocation_id { get; set; }

        public string ValueText { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool? ValueBool { get; set; }
    }
}
