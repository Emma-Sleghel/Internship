using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserOperation.Data.Entities;
using UserOperation.Services.Dtos;
using UserOperation.Services.Services;
using UserOperation.Web.Models;

namespace UserOperation.Web.Controllers
{
    public class LeaveController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILeaveService _leaveService;
        public LeaveController(ILeaveService leaveService,IMapper mapper)
        {
            _leaveService = leaveService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var objLeaveList = _leaveService.GetAllLeaves();
            return View(objLeaveList);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveViewModel model)
        {
            var obj = _mapper.Map<LeaveDto>(model);
            if (ModelState.IsValid)
            {              
                var response = _leaveService.CreateLeave(obj);
                if(response == null)
                {
                    return View(obj);
                }
                else
                {
                    TempData["success"] = "Employee created successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
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
            return View(leaveFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteLeave(int id)
        {          
            if (!_leaveService.DeleteLeave(id))
                ModelState.AddModelError("", "Something went wrong deleting employee");
            TempData["success"] = "Employee deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
