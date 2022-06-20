using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.Domain.ViewModels;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system.Controllers
{
    [Authorize]
    public class WorkingCalendarController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IRequestService _requestService;
        private readonly IEmployeeService _employeeService;
        private readonly ICalendarService _calendarService;

        public WorkingCalendarController(UserManager<Employee> userManager, 
            IRequestService requestService,
            IEmployeeService employeeService,
            ICalendarService calendarService)
        {
            _userManager = userManager;
            _requestService = requestService;
            _employeeService = employeeService;
            _calendarService = calendarService;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var authorizedEmployee = await _userManager.GetUserAsync(User);
            var response = await _employeeService.GetEmployeesInCalendar(authorizedEmployee);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        #region GetRequestsByIdEmployee
        public async Task<List<RequestVM>> GetRequestsByIdEmployee(string idEmployee)
        {
            var response = await _requestService.GetRequestsByIdEmployee(idEmployee);
            return response.Data;
        }
        #endregion

        #region GetHolidays
        public async Task<List<HolidayCalendarVM>> GetHolidays()
        {
            var response = await _calendarService.GetAllHolidays();
            return response.Data;
        }
        #endregion
    }
}
