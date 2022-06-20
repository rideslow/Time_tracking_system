using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Time_tracking_system.Domain.ViewModels;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class AttributesController : Controller
    {
        private readonly IRequestService _requestService;

        public AttributesController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        #region Index
        // GET: Attributes
        public async Task<IActionResult> Index()
        {
            var response = await _requestService.GetAllAttributes();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // GET: Attributes/Create
        #region Create get
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        // POST: Attributes/Create
        #region Create post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttributesVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var response = await _requestService.CreateAttributes(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.CreateError)
            {
                ModelState.AddModelError("", response.Description);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

    }
}
