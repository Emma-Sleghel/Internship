using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserOperation.Data.Entities;
using UserOperation.Data.Repository;
using UserOperation.Services.Dtos;
using UserOperation.Services.Helpers;
using UserOperation.Services.Services;
using UserOperation.Web.Models;

namespace UserOperation.Web.Controllers
{
    public class StabilityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStabilityService _stabilityService;
        private readonly ILogger<StabilityController> _logger;
        private readonly IStabilityHelper _stabilityHelper;

        public StabilityController(IStabilityService stabilityService, IMapper mapper, ILogger<StabilityController> logger, IStabilityHelper stabilityHelper)
        {
            _stabilityService = stabilityService;
            _mapper = mapper;
            _logger = logger; 
            _stabilityHelper = stabilityHelper;
        }
        
        public IActionResult Index()
        {
            var objStabilityList = _stabilityService.GetAllStabilities();
            return View(objStabilityList);
        }
        public IActionResult Create()
        { 
            var projectList = _mapper.Map<List<ProjectViewModel>>(_stabilityHelper.GetProjects());
            ViewBag.ProjectListDB = projectList;

            var levelList = _mapper.Map<List<LevelViewModel>>(_stabilityHelper.GetLevels());
            ViewBag.LevelListDB = levelList;

            var stabilityLevelList = _mapper.Map<List<StabilityLevelViewModel>>(_stabilityHelper.GetStabilityLevels());
            ViewBag.StabilityLevelListDB = stabilityLevelList;

            var criticalityList = _mapper.Map<List<CriticalityViewModel>>(_stabilityHelper.GetCriticalities());
            ViewBag.CriticalityListDB = criticalityList;

            var positionList = _mapper.Map<List<PositionViewModel>>(_stabilityHelper.GetPositions());
            ViewBag.PositionListDB = positionList;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StabilityViewModel model)
        {
            
            var obj = _mapper.Map<StabilityDto>(model);
            if (ModelState.IsValid)
            {              
                _stabilityService.CreateStability(obj);
                TempData["success"] = "Employee created successfully";
                return RedirectToAction("Index");
            }
            return View(_mapper.Map<StabilityViewModel>(obj));
        }
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var stabilityFromDb = _stabilityService.GetStabilityById(id);
            if (stabilityFromDb == null)
            {
                return NotFound();
            }
            return View(stabilityFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StabilityViewModel model)
        { 

            var obj = _mapper.Map<StabilityDto>(model);
            if (ModelState.IsValid)
            {
                _stabilityService.UpdateStability(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            if ( id == 0)
            {
                return NotFound();
            }
            var stabilityFromDb = _stabilityService.GetStabilityById(id);
            if (stabilityFromDb == null)
            {
                return NotFound();
            }
            return View(stabilityFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStability(int id)
        {
            if (!_stabilityService.DeleteStability(id))
                ModelState.AddModelError("","Something went wrong deleting employee");

            return RedirectToAction("Index");
        }
    }
}
