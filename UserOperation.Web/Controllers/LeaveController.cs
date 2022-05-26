using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserOperation.Data.Entities;
using UserOperation.Services.Dtos;
using UserOperation.Services.Helpers;
using UserOperation.Services.Services;
using UserOperation.Web.Models;

namespace UserOperation.Web.Controllers
{
    public class LeaveController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILeaveService _leaveService;
        private readonly ILogger<LeaveController> _logger;
        private readonly IBaseHelper _baseHelper;

        private readonly List<ProjectViewModel> _projects;
        private readonly List<LevelViewModel> _levels;    
        private readonly List<ReasonViewModel> _reasons;
        private readonly List<PositionViewModel> _positions;


        public LeaveController(ILeaveService leaveService,IMapper mapper, ILogger<LeaveController> logger, IBaseHelper baseHelper)
        {
            _leaveService = leaveService;
            _mapper = mapper;
            _logger = logger;
            _baseHelper = baseHelper;

            _projects = _mapper.Map<List<ProjectViewModel>>(_baseHelper.GetProjects());
            _levels = _mapper.Map<List<LevelViewModel>>(_baseHelper.GetLevels());
            _reasons = _mapper.Map<List<ReasonViewModel>>(_baseHelper.GetReasons());
            _positions = _mapper.Map<List<PositionViewModel>>(_baseHelper.GetPositions());

        }
        public void ViewBagAsign(List<ProjectViewModel> _projects, List<LevelViewModel> _levels,
            List<ReasonViewModel> _reasons, List<PositionViewModel> _positions)
        {
            ViewBag.Projects = new MultiSelectList(_projects, "ProjectId", "ProjectName");
            ViewBag.Levels = new SelectList(_levels, "LevelId", "LevelName");
            ViewBag.Positions = new SelectList(_positions, "PositionId", "PositionName");
            ViewBag.Months = new SelectList(new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
            ViewBag.Reasons = new SelectList(_reasons, "ReasonId", "ReasonName");
        }

        public IActionResult Index()
        {
            var objLeaveList = _leaveService.GetAllLeaves();
            return View(objLeaveList);
        }

        public IActionResult Create()
        {
            ViewBagAsign(_projects, _levels, _reasons, _positions);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveViewModel model)
        {
            var obj = _mapper.Map<LeaveDto>(model);
            ViewBagAsign(_projects, _levels, _reasons, _positions);

            if (ModelState.IsValid)
            {
                _leaveService.CreateLeave(obj);
                TempData["success"] = "Employee created successfully";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if( id == 0)
            {
                return NotFound();
            }
            var leaveFromDb = _leaveService.GetLeaveById(id);
            if(leaveFromDb == null)
            {
                return NotFound();
            }
            return View(leaveFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LeaveViewModel model)
        {         
            var obj = _mapper.Map<LeaveDto>(model);
            if (ModelState.IsValid)
            {
                var response = _leaveService.UpdateLeave(obj);
                if(response == null)
                {
                    return View(obj);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            if ( id == 0)
            {
                return NotFound();
            }
            var leaveFromDb = _leaveService.GetLeaveById(id);
            if (leaveFromDb == null)
            {
                return NotFound();
            }
            return PartialView("_DeleteLeavePartial",leaveFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteLeave(int id)
        {
            var leaveFromDb = _leaveService.GetLeaveById(id);

            if (leaveFromDb == null)
            {
                return NotFound();
            }
            _leaveService.DeleteLeave(id);

            if (!_leaveService.DeleteLeave(id))
                ModelState.AddModelError("", "Something went wrong deleting employee");

            return PartialView("_DeleteLeavePartial", leaveFromDb);
        }
    }
}
