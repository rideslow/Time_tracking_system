using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Time_tracking_system.Domain.ViewModels;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class TypeRequestsController : Controller
    {
        private readonly IRequestService _requestService;

        public TypeRequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: TypeRequests
        #region Index
        public async Task<IActionResult> Index()
        {
            var response = await _requestService.GetAllTypeRequest();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // GET: TypeRequests/Create
        #region Create get
        public async Task<IActionResult> Create()
        {
            var response = await _requestService.GetAllAttributes();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewData["Attributes"] = new SelectList(response.Data, "Id", "NameAttributes");
                return View();
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // POST: TypeRequests/Create
        #region Create post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeRequestVM model, List<int> attr)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var response = await _requestService.CreateTypeRequest(model, attr);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.CreateError)
            {
                ModelState.AddModelError("", response.Description);
                return View(model);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // GET: TypeRequests/Details/5
        #region Details
        public async Task<IActionResult> Details(int id)
        {
            var response = await _requestService.DetailsTypeRequest(id);
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

    }
}
