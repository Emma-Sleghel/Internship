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
            _employeeService=employeeService;
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
                    
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Where(w => w.Name == "Leaves").FirstOrDefault();
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowcount; row++)
                    {
                        Leavelist.Add(new LeaveViewModel
                        {
                            Employee = new EmployeeViewModel
                            {
                                EmployeeId = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                EmployeeName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                ProjectsIds = _employeeService.GetProjectsIdsFromString(worksheet.Cells[row, 3].Value.ToString().Trim()),

                                Position = new PositionViewModel
                                {
                                    PositionId = _employeeService.GetPositionId(worksheet.Cells[row, 4].Value.ToString().Trim()),
                                },
                                Level = new LevelViewModel
                                {
                                    LevelId = _employeeService.GetLevelId(worksheet.Cells[row, 5].Value.ToString().Trim()),
                                },


                            },
                            LeaveMonth = worksheet.Cells[row, 6].Value.ToString().Trim(),
                            LeaveYear = Int32.Parse(worksheet.Cells[row, 7].Value.ToString().Trim()),
                            ActiveHC = Int32.Parse(worksheet.Cells[row, 8].Value.ToString().Trim()),
                            PrimaryReason = new ReasonViewModel
                            {
                                ReasonId = _leaveService.GetReasonId(worksheet.Cells[row, 9].Value.ToString().Trim()),
                            },
                            SecondaryReason = new ReasonViewModel
                            {
                                ReasonId = _leaveService.GetReasonId(worksheet.Cells[row, 10].Value.ToString().Trim()),
                            },
                        });
                    }

                    ExcelWorksheet worksheetStability = package.Workbook.Worksheets.Where(w => w.Name == "Stability").FirstOrDefault();
                  var rowCountStability = worksheetStability.Dimension.Rows;
                   
                    for (int row = 2; row <= rowCountStability; row++)
                    {
                        if (worksheetStability.Cells[row, 1].Value == null)
                            break;

                        var projectsIds = _employeeService.GetProjectsIdsFromString(worksheetStability.Cells[row, 3].Value.ToString().Trim());
                        Stabilitylist.Add(new StabilityViewModel
                        {       

                            Employee = new EmployeeViewModel
                            {
                                EmployeeId = worksheetStability.Cells[row, 1].Value.ToString().Trim(),
                                EmployeeName = worksheetStability.Cells[row, 2].Value.ToString().Trim(),

                                ProjectsIds = projectsIds,
                                Projects = MappingProjectsToProjectsViewModel(_employeeService.GetProjectsByIds(projectsIds)),
                                Position = new PositionViewModel
                                {
                                    PositionId = _employeeService.GetPositionId(worksheetStability.Cells[row, 4].Value.ToString().Trim()),
                                },
                                Level = new LevelViewModel
                                {
                                    LevelId = _employeeService.GetLevelId(worksheetStability.Cells[row, 5].Value.ToString().Trim()),
                                },


                            },

                            StabilityMonth = worksheetStability.Cells[row, 6].Value.ToString().Trim(),
                            LeavingYear = Int32.Parse(worksheetStability.Cells[row, 7].Value.ToString().Trim()),
                            StabilityLevel = new StabilityLevelViewModel
                            {
                                StabilityLevelID = _stabilityService.GetStabilityLevelId(worksheetStability.Cells[row, 8].Value.ToString().Trim()),
                            },
                            Criticality = new CriticalityViewModel
                            {
                                CriticalityID = _stabilityService.GetCriticalityId(worksheetStability.Cells[row, 9].Value.ToString().Trim()),
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
            return RedirectToAction("Index","Leave");
        }
        public List<ProjectViewModel> MappingProjectsToProjectsViewModel(List<ProjectDto> projects)
        {
            List<ProjectViewModel> result= new List<ProjectViewModel>();
            foreach (var project in projects)
            {
                result.Add(_mapper.Map<ProjectViewModel>(project));
            }
            return result;
        }

    }
}
