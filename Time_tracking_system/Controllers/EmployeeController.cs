using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.Domain.ViewModels;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IRequestService _requestService;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
            UserManager<Employee> userManager,
            IRequestService requestService,
            IEmployeeService employeeService)
        {
            _userManager = userManager;
            _requestService = requestService;
            _employeeService = employeeService;
        }

        // GET: Employee/Edit/5
        #region Edit Get
        public async Task<IActionResult> Edit()
        {
            var authorizedEmployee = await _userManager.GetUserAsync(User);
            var response = await _employeeService.GetEditEmployees(authorizedEmployee);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // POST: Employee/Edit/5
        #region Edit post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeVM model)
        {
            var response = await _employeeService.EditEmployee(model, User.IsInRole("Администратор"));
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Edit");
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        #region GetEmployeeByIdPV
        public async Task<IActionResult> GetEmployeeByIdPV(string id)
        {
            var response = await _employeeService.GetEmployeeAndRole(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var responseRoles = await _employeeService.GetAllRoles();
                if (responseRoles.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    ViewData["listRole"] = new SelectList(responseRoles.Data, "Id", "Name");
                    return PartialView(response.Data);
                }
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        #region GetStatusInfoByIdPV
        public async Task<IActionResult> GetStatusInfoByIdPV(string id)
        {
            var response = await _requestService.GetStatisticsById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewData["countDayVocationInTheCurrentYear"] = response.Data["countDayVocationInTheCurrentYear"];
                ViewData["countDayVocationInThePastYears"] = response.Data["countDayVocationInThePastYears"];
                ViewData["countDaysOffInTheCurrentYear"] = response.Data["countDaysOffInTheCurrentYear"];
                ViewData["allCountRequest"] = response.Data["allCountRequest"];
                ViewData["countRequestWithStatusNew"] = response.Data["countRequestWithStatusNew"];
                ViewData["countRequestWithStatusApproved"] = response.Data["countRequestWithStatusApproved"];
                ViewData["countRequestWithStatusAgreed"] = response.Data["countRequestWithStatusAgreed"];
                ViewData["countRequestWithStatusNotApproved"] = response.Data["countRequestWithStatusNotApproved"];
                ViewData["countRequestWithStatusNotAgreed"] = response.Data["countRequestWithStatusNotAgreed"];
                ViewData["countRequestWithStatusCanceled"] = response.Data["countRequestWithStatusCanceled"];
                ViewData["countRequestWithStatusWithdrawn"] = response.Data["countRequestWithStatusWithdrawn"];
                return PartialView();
            }
            return View("Error", $"{response.Description}");
        }
        #endregion
    }
}
