using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Response;
using Time_tracking_system.Domain.ViewModels;

namespace Time_tracking_system.Service.Interfaces
{
    public interface ICalendarService
    {
        Task<IBaseResponse<List<YearsVM>>> GetAllYears();
        Task<IBaseResponse<YearAndHolidayCalendarVM>> DetailsHolidayCalendar(int id);
        Task<IBaseResponse<bool>> CreateHolidayCalendar(string selectidDate, string year);
        Task<IBaseResponse<bool>> PutTheCalendarIntoCirculation(int id);
        Task<IBaseResponse<YearAndHolidayCalendarVM>> GetYearAndHolidayCalendar(int id);
        Task<IBaseResponse<bool>> EditHolidayCalendar(string selectidDate);
        Task<IBaseResponse<List<HolidayCalendarVM>>> GetAllHolidays();

    }
}
