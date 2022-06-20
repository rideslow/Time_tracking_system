using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class HolidayCalendarController : Controller
    {
        private readonly ICalendarService _calendarService;

        public HolidayCalendarController(
            ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        // GET: HolidayCalendar
        #region Index
        public async Task<IActionResult> Index()
        {
            var response = await _calendarService.GetAllYears();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // GET: HolidayCalendar/Details/5
        #region Details
        public async Task<IActionResult> Details(int id)
        {
            var response = await _calendarService.DetailsHolidayCalendar(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.NotFound)
            {
                return NotFound();
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // GET: HolidayCalendar/Create
        #region CreateGet
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        // POST: HolidayCalendar/Create
        #region CreatePost
        [HttpPost]
        public async Task<bool> Create(string selectidDate, string year)
        {
            var response = await _calendarService.CreateHolidayCalendar(selectidDate, year);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return response.Data;
            }
            return false;
        }
        #endregion

        //ввод календаря в обращение
        #region PutTheCalendarIntoCirculation
        public async Task<bool> PutTheCalendarIntoCirculation(int id)
        {
            var response = await _calendarService.PutTheCalendarIntoCirculation(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return response.Data;
            }
            return false;
        }
        #endregion

        // GET: HolidayCalendar/Edit/5
        #region EditGet
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _calendarService.GetYearAndHolidayCalendar(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.NotFound)
            {
                return NotFound();
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // POST: HolidayCalendar/Edit/5
        #region EditPost
        [HttpPost]
        public async Task<bool> Edit(string selectidDate)
        {
            var response = await _calendarService.EditHolidayCalendar(selectidDate);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return response.Data;
            }
            return false;
        }
        #endregion
    }
}
