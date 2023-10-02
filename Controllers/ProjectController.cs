using AutoMapper;
using Enquiry.Web.Dtos;
using Enquiry.Web.Models;
using Enquiry.Web.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Enquiry.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProjectController : Controller
    {
        readonly ILogger<ProjectController> _logger;
        readonly IClient _client;
        readonly IMapper _mapper;
        readonly IProject _project;
        readonly IEmployee _employee;
        public ProjectController(IClient client, ILogger<ProjectController> logger, IMapper mapper, IProject project, IEmployee employee)
        {
            _client = client;
            _logger = logger;
            _mapper = mapper;
            _project = project;
            _employee = employee;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPhase()
        {
            var data = await _project.GetPhaseAsync();
            return PartialView("_GetPhase", data.Data);
        }

        [HttpGet]
        public async Task<IActionResult> IsRegistered(int enquiryId)
        {
            var data = await _client.GetClientDetailAsync(enquiryId);
            if (data.Succeeded)
            {
                var createClient = new CreateProjectDto();
                var map = _mapper.Map<ClientDetailsDto>(data.Data);
                createClient.ClientDetailsDto = map;

                ViewBag.paymentMode = System.Enum.GetValues(typeof(Enum.PaymentMode)).Cast<Enum.PaymentMode>()
                    .Select(enu => new SelectListItem() { Text = enu.ToString(), Value = ((int)enu).ToString() }).ToList();

                ViewBag.workStatus = System.Enum.GetValues(typeof(Enum.WorkStatus)).Cast<Enum.WorkStatus>()
                    .Select(enu => new SelectListItem() { Text = enu.ToString(), Value = ((int)enu).ToString() }).ToList();

                return PartialView("_CreateProject", createClient);
            }
            return Json(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(CreateProjectDto projectDto)
        {
            if (!ModelState.IsValid) return new JsonResult(new { Success = false, Error = "Error while creating project!" });
            projectDto.CreatePayment.PaymentDate = projectDto.RegistrationDate;
            try
            {
                var data = await _project.CreateProjectAsync(projectDto);
                if (data.Succeeded)
                {
                    await _project.CreatePhaseAsync(projectDto.CreatePhaseDto, data.Project.ProjectId);
                    await _project.CreatePaymentAsync(projectDto.CreatePayment, data.Project.ProjectId);
                    await _client.IsRegisteredAsync(projectDto.ClientDetailsDto.EnquiryId, true);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetRegisteredClientDetail(int enquiryId, int projectId)
        {
            var result = await _project.GetRegisteredClientDetailAsync(enquiryId);
            result.Client.ProjectId = projectId;
            
          

            ViewBag.projects = result.Client.ProjectDetailDto.Select(x => new SelectListItem()
            {
                Text = $"{x.ProjectRef} | {x.ProjectName}".ToString(),
                Value = x.ProjectId.ToString()
            }).ToList();

            return PartialView("_GetRegisteredClientDetail", result.Client);
        }


        [HttpGet]
        public async Task<IActionResult> GetProjectDetail(int projectId)
        {
            var result = await _project.GetProjectDetailAsync(projectId);

            return PartialView("_GetProjectDetail", result.Project);
        }


        [HttpGet]
        public IActionResult CreatePhaseForm(int projectId)
        {
            ViewBag.projectId = projectId;
            return PartialView("_CreatePhaseForm");
        }


        [HttpGet]
        public async Task<IActionResult> EditPhaseForm(int phaseId)
        {

            var tec = await _employee.GetTechExpertAsync();
            var pgmer = await _employee.GetProgrammerAsync();

            ViewBag.techExpert = tec.Data.Select(x => new SelectListItem() { Text = x.EmployeeName, Value = x.EmpId.ToString() }).ToList();
            ViewBag.programmer = pgmer.Data.Select(x => new SelectListItem() { Text = x.EmployeeName, Value = x.EmpId.ToString() }).ToList();

            var result = await _project.GetPhaseAsync(phaseId);

            return PartialView("_EditPhaseForm", result.Phase);
        }


        [HttpGet]
        public IActionResult CreatePaymentForm(int projectId)
        {
            ViewBag.paymentMode = System.Enum.GetValues(typeof(Enum.PaymentMode)).Cast<Enum.PaymentMode>()
                    .Select(enu => new SelectListItem() { Text = enu.ToString(), Value = ((int)enu).ToString() }).ToList();
            ViewBag.projectId = projectId;
            return PartialView("_CreatePaymentForm");
        }


        [HttpGet]
        public IActionResult EditPaymentForm()
        {
            ViewBag.paymentMode = System.Enum.GetValues(typeof(Enum.PaymentMode)).Cast<Enum.PaymentMode>()
                    .Select(enu => new SelectListItem() { Text = enu.ToString(), Value = ((int)enu).ToString() }).ToList();
            return PartialView("_EditPaymentForm");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(int projectId, CreatePayment payment)
        {
            if (!ModelState.IsValid) return new JsonResult(new { Success = false, Error = "Error while creating payment!" });
            var result = await _project.CreatePaymentAsync(payment, projectId);
            return Json(new { result.Phase });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhase(int projectId, CreatePhaseDto phase)
        {
            if (!ModelState.IsValid) return new JsonResult(new { Success = false, Error = "Error while creating Phase!" });
            var result = await _project.CreatePhaseAsync(phase, projectId);
            return Json(new { result.Phase });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhase(EditPhaseDto phase)
        {
            if (!ModelState.IsValid) return new JsonResult(new { Success = false, Error = "Error while updating Phase!" });
            var result = await _project.UpdatePhaseAsync(phase);
            return Json(new { result.Phase });
        }

        [HttpGet]
        public IActionResult GetProjectOnHoldForm(int phaseId)
        {
            ViewBag.PhaseId = phaseId;
            return PartialView("_GetProjectOnHoldForm");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhaseOnHold(int phaseId, EditPhaseOnHoldDto phase)
        {
            if (!ModelState.IsValid) return new JsonResult(new { Success = false, Error = "Error while updating Phase!" });
            var result = await _project.UpdatePhaseOnHoldAsync(phase, phaseId);
            return Json(new { result.Phase });
        }

        [HttpGet]
        public async Task<IActionResult> EditProjectOnHoldForm(int phaseId)
        {
            var result = await _project.EditProjectOnHoldFormAsync(phaseId);
            ViewBag.PhaseId = phaseId;
            return PartialView("_EditProjectOnHoldForm");
        }

        [HttpPost]
        public async Task<IActionResult> EditPhaseOnHold(int phaseId, EditPhaseOnHoldDto phase)
        {
            if (!ModelState.IsValid) return new JsonResult(new { Success = false, Error = "Error while updating Phase on hold!" });
            var result = await _project.UpdatePhaseOnHoldAsync(phase, phaseId);
            return Json(new { result.Phase });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProgress(int progressId, int phaseId, int value)
        {
            var result = await _project.UpdateProgressAsync(progressId, phaseId, value);
            ViewBag.PhaseId = phaseId;
            return Json(new { result.Phase });
        }
    }
}
