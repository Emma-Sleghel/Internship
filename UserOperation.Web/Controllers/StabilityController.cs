using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserOperation.Data.Entities;
using UserOperation.Services.Dtos;
using UserOperation.Services.Services;
using UserOperation.Web.Models;

namespace UserOperation.Web.Controllers
{
    public class StabilityController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IStabilityService _stabilityService;
        public StabilityController(IStabilityService stabilityService, IMapper mapper)
        {
            _stabilityService = stabilityService;
            _mapper = mapper;
        }
        
        public IActionResult Index()
        {
            var objStabilityList = _stabilityService.GetAllStabilities();
            return View(objStabilityList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StabilityViewModel model)
        {
            
            //getbyid
            var obj = _mapper.Map<StabilityDto>(model);
            if (ModelState.IsValid)
            {  
                _stabilityService.CreateStability(obj);
                TempData["success"] = "Employee created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
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
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StabilityViewModel model)
        { 
            var stability = _mapper.Map<List<StabilityViewModel>>(_stabilityService.GetAllStabilities())
                 .Where(l => l.Employee.EmployeeName.Trim().ToUpper() == model.Employee.EmployeeName.TrimEnd().ToUpper())
             .FirstOrDefault();
            if (stability != null)
            {
                ModelState.AddModelError("Name", "Exist already an employee with this Name");
            }
            //getbyid
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
            if (id == null || id == 0)
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
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStability(int id)
        {
            if (!_stabilityService.DeleteStability(id))
                ModelState.AddModelError("","Something went wrong deleting employee");

            return RedirectToAction("Index");
        }
    }
}
