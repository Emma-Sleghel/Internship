using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
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
        private readonly IBaseHelper _baseHelper;
        private readonly List<ProjectViewModel> _projects;
        private readonly List<LevelViewModel> _levels;
        private readonly List<StabilityLevelViewModel> _stabilityLevels;
        private readonly List<CriticalityViewModel> _criticalities;
        private readonly List<PositionViewModel> _positions;

        public StabilityController(IStabilityService stabilityService, IMapper mapper, ILogger<StabilityController> logger, IBaseHelper baseHelper)
        {
            _stabilityService = stabilityService;
            _mapper = mapper;
            _logger = logger;
            _baseHelper = baseHelper;
            _projects = _mapper.Map<List<ProjectViewModel>>(_baseHelper.GetProjects());
            _levels = _mapper.Map<List<LevelViewModel>>(_baseHelper.GetLevels());
            _stabilityLevels = _mapper.Map<List<StabilityLevelViewModel>>(_baseHelper.GetStabilityLevels());
            _criticalities = _mapper.Map<List<CriticalityViewModel>>(_baseHelper.GetCriticalities());
            _positions = _mapper.Map<List<PositionViewModel>>(_baseHelper.GetPositions());
        }
        public void ViewBagAsign(List<ProjectViewModel> _projects, List<LevelViewModel> _levels, List<StabilityLevelViewModel> _stabilityLevels
            , List<CriticalityViewModel> _criticalities, List<PositionViewModel> _positions)
        {
            ViewBag.Projects = new MultiSelectList(_projects, "ProjectId", "ProjectName");
            ViewBag.Levels = new SelectList(_levels, "LevelId", "LevelName");
            ViewBag.StabilityLevels = new SelectList(_stabilityLevels, "StabilityLevelID", "StabilityLevelName");
            ViewBag.Criticalities = new SelectList(_criticalities, "CriticalityID", "CriticalityName");
            ViewBag.Positions = new SelectList(_positions, "PositionId", "PositionName");
            ViewBag.Months = new SelectList(new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
        }
        public IActionResult Index()
        {
            var objStabilityList = _stabilityService.GetAllStabilities();
            return View(objStabilityList);
        }


        //[HttpPost]
        //public  ActionResult Export(List<string> rows)
        //{

        //     rows.ForEach(x =>
        //    {
        //        var row = x.Split('-');
        //        var id = row[0];
        //        var name = row[1];
        //        var projects = row[2];
        //        var position = row[3];
        //        var level = row[4];
        //        var month = row[5];
        //        var year = row[6];
        //        var slevel = row[7];
        //        var crit = row[8];

        //    });
        //    if (rows.Count == 0)
        //    {
        //        return NotFound();
        //    }
        //    return  Json(true);
        //}

        [HttpPost]
        public IActionResult Export(List<string> rows)
        {

            rows.ForEach(x =>
            {
                var row = x.Split('-');
                var id = row[0];
                var name = row[1];
                var projects = row[2];
                var position = row[3];
                var level = row[4];
                var month = row[5];
                var year = row[6];
                var slevel = row[7];
                var crit = row[8];
            });

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[9]
            {
                new DataColumn("Personnel No."),
                new DataColumn("Employee name"),
                new DataColumn("Project"),
                new DataColumn("Position Name"),
                new DataColumn("Employee Level"),
                new DataColumn("Stability"),
                new DataColumn("Leaving year"),
                new DataColumn("Level of stability"),
                new DataColumn("Critically")
            });
            if (rows.Count > 0)
            {
                string[] s = rows[0].Split("-");
                dt.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8]);
            }
            //dt.Rows.Add("s", "s", "s", "s", "s", "s", "s", "s", "s");
            //foreach (var row in rows)
            //{
            //    dt.Rows.Add("s", "s", "s", "s", "s", "s", "s", "s", "s");
            //    //dt.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8]);
            //}

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //stream.Position = 0;
                    //TempData["file"] = stream.ToArray();
                    
                    //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "myfile.xlsx");
                    return File(stream.ToArray(), "application/vnd.ms-excel", "myfile.xlsx");
                    
                }
            }
            //return new JsonResult("TestReportOutput.xlsx");
        }
        //[HttpGet]
        //public virtual ActionResult Download(string fileName)
        //{
        //    if (TempData["file"] != null)
        //    {
        //        byte[] data = TempData["file"] as byte[];
        //        return File(data, "application/vnd.ms-excel", fileName);
        //    }
        //    else
        //    {
        //        // Problem - Log the error, generate a blank file,//           redirect to another controller action - whatever fits with your application
        //         return new EmptyResult();
        //    }
        //}




        public IActionResult Create()
        {
            ViewBagAsign(_projects, _levels, _stabilityLevels, _criticalities, _positions);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StabilityViewModel model)
        {
            var obj = _mapper.Map<StabilityDto>(model);
            ViewBagAsign(_projects, _levels, _stabilityLevels, _criticalities, _positions);

            if (ModelState.IsValid)
            {
                _stabilityService.CreateStability(obj);
                TempData["success"] = "Employee created successfully";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var stabilityFromDb = _mapper.Map<StabilityViewModel>(_stabilityService.GetStabilityById(id));
            if (stabilityFromDb == null)
            {
                return NotFound();
            }
            ViewBagAsign(_projects, _levels, _stabilityLevels, _criticalities, _positions);
            return View(stabilityFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StabilityViewModel model)
        {

            var obj = _mapper.Map<StabilityDto>(model);
            if (ModelState.IsValid)
            {
                ViewBagAsign(_projects, _levels, _stabilityLevels, _criticalities, _positions);
                _stabilityService.UpdateStability(obj);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
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
            return PartialView("_DeleteEmployeePartial", stabilityFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStability(int id)
        {
            var stabilityFromDb = _stabilityService.GetStabilityById(id);
            if (stabilityFromDb == null)
            {
                return NotFound();
            }
            _stabilityService.DeleteStability(id);

            if (!_stabilityService.DeleteStability(id))
                ModelState.AddModelError("", "Something went wrong deleting employee");

            return PartialView("_DeleteEmployeePartial", stabilityFromDb);
        }
    }
}
