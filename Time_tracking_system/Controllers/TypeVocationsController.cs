using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Time_tracking_system.Domain.ViewModels;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class TypeVocationsController : Controller
    {
        private readonly IRequestService _requestService;

        public TypeVocationsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: TypeVocations
        #region Index
        public async Task<IActionResult> Index()
        {
            var response = await _requestService.GetAllTypeVocation();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        #endregion

        // GET: TypeVocations/Create
        #region Create get
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        // POST: TypeVocations/Create
        #region Create post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeVocationVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var response = await _requestService.CreateTypeVocation(model);
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
    }
}
