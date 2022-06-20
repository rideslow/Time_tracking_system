using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.DAL.Interfaces
{
    public interface IHolidayCalendarRepository
    {
        Task<bool> Create(HolidayCalendar entity);
        Task<bool> Delete(HolidayCalendar entity);
        Task<ICollection<HolidayCalendar>> FindAll();
        Task<ICollection<HolidayCalendar>> FindAllByIdYear(int idYear);
        //количество праздничных дней в диапазоне дат
        Task<int> NumberOfHolidaysInTheDateRange(DateTime startDate, DateTime endDate);
        Task<bool> Save();
    }
}
