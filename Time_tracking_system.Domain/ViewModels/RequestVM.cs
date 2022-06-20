using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.Domain.ViewModels
{
    public class RequestVM
    {
        public int Id { get; set; }

        [Display(Name = "Тип заявки")]
        public TypeRequest TypeRequest { get; set; }
        public int TypeRequest_id { get; set; }

        [Display(Name = "Податель")]
        public Employee Applicant { get; set; }
        public string Applicant_id { get; set; }

        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата окончания")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Комментарий")]
        public string Comments { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Дата изменения")]
        public DateTime DateRequested { get; set; }

        [Display(Name = "Количество дней")]
        public int? CountDayOnRequest { get; set; }

        [Display(Name = "Согласующий")]
        public Employee Cordinating { get; set; }
        public string Cordinating_id { get; set; }

        [Display(Name = "Статус")]
        public StatusRequest StatusRequest { get; set; }
        public int StatusRequest_id { get; set; }
    }

    
    public class CreateRequestVM
    {
        public IEnumerable<SelectListItem> typeRequest { get; set; }

        [Display(Name = "Тип заявки")]
        [Required]
        public int TypeRequest_id { get; set; }

        [Display(Name = "Дата начала")]
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Display(Name = "Дата окончания")]
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Today;

        [Display(Name = "Комментарий")]
        [MaxLength(300)]
        public string Comments { get; set; }

        public List<Attributes_TypeRequestVM> attributes_TypeRequests { get; set; }
        public List<Value_Attributes_TypeRequestVM> value_Attributes_TypeRequests { get; set; }
    }

    public class AttrAndRequestVM
    {
        public RequestVM request { get; set; }
        public List<ValueRequestAttributesVM> listValueRequestAttr { get; set; }
    }
}
