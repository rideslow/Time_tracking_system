using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_tracking_system.Domain.Entity
{
    //Значение атрибутов заявки
    public class ValueRequestAttributes
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Request_id")]
        public Request Request { get; set; }
        public int? Request_id { get; set; }

        [ForeignKey("Attr_TypeRequest_id")]
        public Attributes_TypeRequest Attributes_TypeRequest { get; set; }
        public int? Attr_TypeRequest_id { get; set; }

        [ForeignKey("ValueEmloyee_id")]
        public Employee Employee { get; set; }
        public string ValueEmloyee_id { get; set; }

        [ForeignKey("ValueTypeVocation_id")]
        public TypeVocation TypeVocation { get; set; }
        public int? ValueTypeVocation_id { get; set; }

        public string ValueText { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool? ValueBool { get; set; }
    }
}
