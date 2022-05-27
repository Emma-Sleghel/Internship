using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using UserOperation.Services.Dtos;
using UserOperation.Services.Services;
using UserOperation.Web.Models;

namespace UserOperation.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStabilityService _stabilityService;
        private readonly ILeaveService _leaveService;
        private readonly IEmployeeService _employeeService;
        public FileController(IStabilityService stabilityService, IMapper mapper, ILeaveService leaveService, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _stabilityService = stabilityService;
            _leaveService = leaveService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Import(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var Stabilitylist = new List<StabilityViewModel>();
            var Leavelist = new List<LeaveViewModel>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheetLeaves = package.Workbook.Worksheets.Where(w => w.Name == "Leaves").FirstOrDefault();
                    var rowcount = worksheetLeaves.Dimension.Rows;
                    for (int row = 2; row <= rowcount; row++)
                    {
                        if (worksheetLeaves.Cells[row, 1].Value == null)
                            break;
                        var employeeId = worksheetLeaves.Cells[row, 1].Value.ToString().Trim();
                        var employeeName = worksheetLeaves.Cells[row, 2].Value.ToString().Trim();
                        var projectsIds = _employeeService.GetProjectsIdsFromString(worksheetLeaves.Cells[row, 3].Value.ToString().Trim());
                        var positionId = _employeeService.GetPositionId(worksheetLeaves.Cells[row, 4].Value.ToString().Trim());
                        var levelId = _employeeService.GetLevelId(worksheetLeaves.Cells[row, 5].Value.ToString().Trim());
                        var leaveMonth = worksheetLeaves.Cells[row, 6].Value.ToString().Trim();
                        var leaveYear = Int32.Parse(worksheetLeaves.Cells[row, 7].Value.ToString().Trim());
                        var activeHC = Int32.Parse(worksheetLeaves.Cells[row, 8].Value.ToString().Trim());
                        var reasonId = _leaveService.GetReasonId(worksheetLeaves.Cells[row, 9].Value.ToString().Trim());
                        Leavelist.Add(new LeaveViewModel
                        {
                            Employee = new EmployeeViewModel
                            {
                                EmployeeId = employeeId,
                                EmployeeName = employeeName,
                                ProjectsIds=projectsIds,
                                Position = new PositionViewModel
                                {
                                    PositionId = positionId,
                                },
                                Level = new LevelViewModel
                                {
                                    LevelId = levelId,
                                },
                            },
                            LeaveMonth=leaveMonth,
                            LeaveYear=leaveYear,
                            ActiveHC=activeHC,
                            PrimaryReason = new ReasonViewModel
                            {
                               ReasonId = reasonId,
                            },
                            SecondaryReason = new ReasonViewModel
                            {
                                ReasonId= reasonId,
                            },
                        });
                    }
                    ExcelWorksheet worksheetStability = package.Workbook.Worksheets.Where(w => w.Name == "Stability").FirstOrDefault();
                    var rowCountStability = worksheetStability.Dimension.Rows;
                    for (int row = 2; row <= rowCountStability; row++)
                    {
                        if (worksheetStability.Cells[row, 1].Value == null)
                            break;
                        var employeeId = worksheetStability.Cells[row, 1].Value.ToString().Trim();
                        var employeeName = worksheetStability.Cells[row, 2].Value.ToString().Trim();
                        var projectsIds = _employeeService.GetProjectsIdsFromString(worksheetStability.Cells[row, 3].Value.ToString().Trim());
                        var positionId = _employeeService.GetPositionId(worksheetStability.Cells[row, 4].Value.ToString().Trim());
                        var levelId= _employeeService.GetLevelId(worksheetStability.Cells[row, 5].Value.ToString().Trim());
                        var stabilityMonth = worksheetStability.Cells[row, 6].Value.ToString().Trim();
                        var leavingYear = Int32.Parse(worksheetStability.Cells[row, 7].Value.ToString().Trim());
                        var stabilityLevelID = _stabilityService.GetStabilityLevelId(worksheetStability.Cells[row, 8].Value.ToString().Trim());
                        var criticalityID = _stabilityService.GetCriticalityId(worksheetStability.Cells[row, 9].Value.ToString().Trim());
                        Stabilitylist.Add(new StabilityViewModel
                        {

                            Employee = new EmployeeViewModel
                            {
                                EmployeeId = employeeId,
                                EmployeeName = employeeName,
                                ProjectsIds = projectsIds,
                                Position = new PositionViewModel
                                {
                                    PositionId = positionId,
                                },
                                Level = new LevelViewModel
                                {
                                    LevelId = levelId,
                                },
                            },
                            StabilityMonth = stabilityMonth,
                            LeavingYear = leavingYear,
                            StabilityLevel = new StabilityLevelViewModel
                            {
                                StabilityLevelID = stabilityLevelID,
                            },
                            Criticality = new CriticalityViewModel
                            {
                                CriticalityID = criticalityID,
                            }
                        }) ;
                    }
                }
            }
            foreach (var item in Stabilitylist)
            {
                _stabilityService.CreateStability(_mapper.Map<StabilityDto>(item));
            }
            foreach (var item in Leavelist)
            {
                _leaveService.CreateLeave(_mapper.Map<LeaveDto>(item));
            }
            return RedirectToAction("Index", "Leave");
        }
    }
}
