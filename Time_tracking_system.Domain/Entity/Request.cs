using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_tracking_system.Domain.Entity
{
    //Заявка
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TypeRequest_id")]
        public TypeRequest TypeRequest { get; set; }
        public int TypeRequest_id { get; set; }

        [ForeignKey("Applicant_id")]
        public Employee Applicant { get; set; }
        public string Applicant_id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comments { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateRequested { get; set; }

        public int? CountDayOnRequest { get; set; }

        [ForeignKey("Cordinating_id")]
        public Employee Cordinating { get; set; }
        public string Cordinating_id { get; set; }

        [ForeignKey("StatusRequest_id")]
        public StatusRequest StatusRequest { get; set; }
        public int StatusRequest_id { get; set; }

    }
}
