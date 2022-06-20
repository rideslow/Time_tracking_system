using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Time_tracking_system.Domain.ViewModels
{
    public class EmployeeVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [Display(Name = "Имя")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Должность")]
        public string Post { get; set; }

        [Display(Name = "ФИО")]
        public string FullName { get; set; }
    }
    public class CreateRequestEmployeeVM
    {
        public int index;
        [Display(Name = "Список сотрудников")]
        public List<EmployeeVM> listEmployee { get; set; }
    }

    public class EditEmployeeVM
    {
        [Display(Name = "Роль")]
        public string RoleId { get; set; }
        public EmployeeVM EmpVM { get; set; }
    }
}
