using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_tracking_system.Domain.Entity
{
    //Тип заявки -> соответствующие атрибуты
    public class Attributes_TypeRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TypeRequest_id")]
        public TypeRequest TypeRequest { get; set; }
        public int TypeRequest_id { get; set; }

        [ForeignKey("Attr_id")]
        public Attributes Attributes { get; set; }
        public int Attr_id { get; set; }
    }
}
