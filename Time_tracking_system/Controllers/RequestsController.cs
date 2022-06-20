using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class RequestsController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<Employee> _userManager;

        public RequestsController(
            IRequestService requestService,
            IEmployeeService employeeService,
            UserManager<Employee> userManager)
        {
            _requestService = requestService;
            _employeeService = employeeService;
            _userManager = userManager;
        }

        // GET: Requests
        #region Index        
        public async Task<IActionResult> Index()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError("", TempData["CustomError"].ToString());
            }
            if (User.IsInRole("Сотрудник"))
            {
                var employee = await _userManager.GetUserAsync(User);
                var response = await _requestService.GetAllApplicationsWithTheRoleEmployeeByUserId(employee.Id);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(response.Data);
                }
                return View("Error", $"{response.Description}");
            }
            else
            {
                var responseWithoutStatusNew = await _requestService.GetAllRequestsWithoutStatusNew();
                if (responseWithoutStatusNew.StatusCode != Domain.Enum.StatusCode.OK)
                {
                    return View("Error", $"{responseWithoutStatusNew.Description}");
                }
                var employee = await _userManager.GetUserAsync(User);
                var response = await _requestService.GetAllApplicationsWithTheRoleEmployeeByUserId(employee.Id);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    var responseEmployeeAndWithoutStatusNew = responseWithoutStatusNew.Data.Union(response.Data);
                    return View(responseEmployeeAndWithoutStatusNew);
                }
                return View("Error", $"{response.Description}");
            }
        }
        #endregion

        // GET: Requests/Details/5
        #region Details
        public async Task<IActionResult> Details(int id)
        {
            var response = await _requestService.GetAttrAndRequestById(id);
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

        // GET: Requests/Create
        #region Create get
        public async Task<ActionResult> Create()
        {
            var response = await _requestService.GetAttrAndTypeRequest();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        #region GetTypeVocation
        public async Task<ActionResult> GetTypeVocation(int i)
        {
            var response = await _requestService.GetAllTypeVocation();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var model = new CreateRequestTypeVocationVM
                {
                    index = i,
                    typeVocations = response.Data
                };
                return PartialView(model);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        #region GetEmployee
        public async Task<ActionResult> GetEmployee(int i)
        {
            var response = await _employeeService.GetAllEmployees();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var model = new CreateRequestEmployeeVM
                {
                    index = i,
                    listEmployee = response.Data
                };
                return PartialView(model);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        #region Create post
        // POST: Requests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRequestVM model)
        {
                var response = await _requestService.GetAttrAndTypeRequest();
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    model.attributes_TypeRequests = response.Data.attributes_TypeRequests;
                    model.typeRequest = response.Data.typeRequest;
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }
                    var employee = await _userManager.GetUserAsync(User);
                    var responseCreateRequest = await _requestService.CreateRequest(model, employee.Id);
                    if (responseCreateRequest.StatusCode == Domain.Enum.StatusCode.OK &&
                        responseCreateRequest.Data == true)
                    {
                        return RedirectToAction("Index");
                    }
                    else if (responseCreateRequest.StatusCode == Domain.Enum.StatusCode.OK &&
                        responseCreateRequest.Data == false)
                    {
                        ModelState.AddModelError("", responseCreateRequest.Description);
                        return View(model);
                    }
                }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // GET: Requests/Edit/5
        #region Edit get
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _requestService.GetAttrAndRequestById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var responseTypeVocation = await _requestService.GetAllTypeVocation();
                var responseEmployees = await _employeeService.GetAllEmployees();
                if(responseTypeVocation.StatusCode == Domain.Enum.StatusCode.OK &&
                    responseEmployees.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    ViewData["TypeVocation"] = new SelectList(responseTypeVocation.Data, "Id", "Name");
                    ViewData["Employees"] = new SelectList(responseEmployees.Data, "Id", "FullName");
                    return View(response.Data);
                }
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.NotFound)
            {
                return NotFound();
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // POST: Requests/Edit/5
        #region Edit post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AttrAndRequestVM model)
        {
            var responseTypeVocation = await _requestService.GetAllTypeVocation();
            var responseEmployees = await _employeeService.GetAllEmployees();
            if (responseTypeVocation.StatusCode == Domain.Enum.StatusCode.OK &&
                    responseEmployees.StatusCode == Domain.Enum.StatusCode.OK)
            {
                if (!ModelState.IsValid)
                {
                    ViewData["TypeVocation"] = new SelectList(responseTypeVocation.Data, "Id", "Name");
                    ViewData["Employees"] = new SelectList(responseEmployees.Data, "Id", "FullName");
                    return View(model);
                }
                var response = await _requestService.EditRequest(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK &&
                    response.Data == true)
                {
                    return RedirectToAction("Index");
                }
                else if(response.StatusCode == Domain.Enum.StatusCode.OK &&
                    response.Data == false)
                {
                    ModelState.AddModelError("", response.Description);
                    ViewData["TypeVocation"] = new SelectList(responseTypeVocation.Data, "Id", "Name");
                    ViewData["Employees"] = new SelectList(responseEmployees.Data, "Id", "FullName");
                    return View(model);
                }
            }
            return View("Error", $"");
        }
        #endregion

        #region SubmitForReview
        public async Task<IActionResult> SubmitForReview(int id)
        {
            var response = await _requestService.SubmitForReviewRequest(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK &&
                    response.Data == true)
            {
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.OK &&
                response.Data == false)
            {
                TempData["CustomError"] = response.Description.ToString();
                return RedirectToAction("Index");
            }
            else if(response.StatusCode == Domain.Enum.StatusCode.NotFound)
            {
                return NotFound();
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        #region AgreedReview
        [Authorize(Roles = "Руководитель")]
        public async Task<bool> AgreedReview(int id)
        {
            var employee = await _userManager.GetUserAsync(User);
            var response = await _requestService.SetStatusRequest(id, employee.Id, "Agreed");
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return response.Data;
            }
            return false;
        }
        #endregion

        #region NotAgreedReview
        [Authorize(Roles = "Руководитель")]
        public async Task<bool> NotAgreedReview(int id)
        {
            var employee = await _userManager.GetUserAsync(User);
            var response = await _requestService.SetStatusRequest(id, employee.Id, "NotAgreed");
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return response.Data;
            }
            return false;
        }
        #endregion

        #region ApprovedReview
        [Authorize(Roles = "Директор")]
        public async Task<bool> ApprovedReview(int id)
        {
            var employee = await _userManager.GetUserAsync(User);
            var response = await _requestService.SetStatusRequest(id, employee.Id, "Approved");
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return response.Data;
            }
            return false;
        }
        #endregion

        #region NotApprovedReview
        [Authorize(Roles = "Директор")]
        public async Task<bool> NotApprovedReview(int id)
        {
            var employee = await _userManager.GetUserAsync(User);
            var response = await _requestService.SetStatusRequest(id, employee.Id, "NotApproved");
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return response.Data;
            }
            return false;
        }
        #endregion

        //Отозвать заявку
        #region WithdrawRequest
        public async Task<IActionResult> WithdrawRequest(int id)
        {
            var employee = await _userManager.GetUserAsync(User);
            var response = await _requestService.SetStatusRequest(id, employee.Id, "Withdraw");
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.UpdateError)
            {
                TempData["CustomError"] = response.Description.ToString();
                return RedirectToAction("Index");
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        //Отменить заявку
        #region CancelRequest
        public async Task<IActionResult> CancelRequest(int id)
        {
            var employee = await _userManager.GetUserAsync(User);
            var response = await _requestService.SetStatusRequest(id, employee.Id, "Cancel");
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.UpdateError)
            {
                TempData["CustomError"] = response.Description.ToString();
                return RedirectToAction("Index");
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        //подтверждение отмены руководителем
        #region CancellationConfirmationByManager
        [Authorize(Roles = "Руководитель")]
        public async Task<bool> CancellationConfirmationByManager(int id)
        {
            var employee = await _userManager.GetUserAsync(User);
            var response = await _requestService.SetStatusRequest(id, employee.Id, "CancellationConfirmationByManager");
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return response.Data;
            }
            return false;
        }
        #endregion

        //подтверждение отмены директором
        #region CancellationConfirmationByDirector
        [Authorize(Roles = "Директор")]
        public async Task<bool> CancellationConfirmationByDirector(int id)
        {
            var employee = await _userManager.GetUserAsync(User);
            var response = await _requestService.SetStatusRequest(id, employee.Id, "CancellationConfirmationByDirector");
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return response.Data;
            }
            return false;
        }
        #endregion

    }
}
