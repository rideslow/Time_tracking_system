using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.Domain.ViewModels
{
    public class HolidayCalendarVM
    {
        public int Id { get; set; }
        public DateTime HolidayDate { get; set; }
                
        public Years Years { get; set; }
        public int Years_id { get; set; }

        //Статус для понимания, новая дата, измененная или удаленная в календаре, после редактирования
        //Existing - уже существует в бд
        //New - новая (возвращается от клиента
        //Removed - удаляемая
        public string Status { get; set; } = "Existing";

    }


    public class YearAndHolidayCalendarVM
    {
        public List<HolidayCalendarVM> ListHolidayCalendar { get; set; }

        [Display(Name = "Год")]
        public YearsVM Year { get; set; }
    }
}
