using System.ComponentModel.DataAnnotations;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.Domain.ViewModels
{
    public class Attributes_TypeRequestVM
    {
        public int Id { get; set; }

        [Display(Name = "Тип заявки")]
        public TypeRequest TypeRequest { get; set; }
        public int TypeRequest_id { get; set; }

        [Display(Name = "Атрибут")]
        public Attributes Attributes { get; set; }
        public int Attr_id { get; set; }
    }

    public class Value_Attributes_TypeRequestVM
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
