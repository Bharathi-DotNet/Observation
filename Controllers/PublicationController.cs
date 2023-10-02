using Enquiry.Web.Dtos;
using Enquiry.Web.Models;
using Enquiry.Web.Services;
using Enquiry.Web.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Enquiry.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PublicationController : Controller
    {
        readonly IPublication _publication;
        readonly ILogger<PublicationController> _logger;
        readonly IProject _project;
        private readonly ISession _session;

        public PublicationController(IPublication publication, ILogger<PublicationController> logger, IProject project, IHttpContextAccessor httpContextAccessor)
        {
            _publication = publication;
            _logger = logger;
            _project = project;
            _session = httpContextAccessor.HttpContext.Session;

            if (_session.GetString("PublishedFilter") is null)
            {
                _session.SetString("PublishedFilter", "A");  //Default is Published
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PublicationsAsync(char chkValue)
        {

            if (chkValue == '\0')
            {
                chkValue = _session.GetString("PublishedFilter").ToCharArray()[0];
            }

            var data = await _publication.GetPublicationListAsync(chkValue);
            _session.SetString("PublishedFilter", chkValue.ToString());

            return PartialView("_Publications", data.Publication);
        }

        [HttpGet]
        public async Task<IActionResult> GetPublicationClientDetail(int enquiryId, int projectId)
        {
            var result = await _project.GetRegisteredClientDetailAsync(enquiryId);
            result.Client.ProjectId = projectId;

            ViewBag.projects = result.Client.ProjectDetailDto.Select(x => new SelectListItem()
            {
                Text = $"{x.ProjectRef} | {x.ProjectName} | {x.RegistrationDate.ToShortDateString()}".ToString(),
                Value = x.ProjectId.ToString()
            }).ToList();

            return PartialView("_PublicationDetail", result.Client);
        }

        public async Task<IActionResult> GetPayments(int projectId)
        {
            var result = await _project.GetProjectDetailAsync(projectId);
            return PartialView("_GetPayments", result.Project);
        }
        [HttpGet]
        public async Task<IActionResult> GetJournalDetails(int projectId)
        {
            ViewBag.projectId = projectId;
            var jnl = await _publication.GetJournalListAsync(projectId);
            return PartialView("_GetJournalDetails", jnl.Data);
        }

        [HttpGet]
        public IActionResult CreateJournalForm(int projectId)
        {
            var createJournal = new JournalDetailDto();
            createJournal.ProjectId= projectId;
            createJournal.PublicationId = 0;
            ViewBag.JournalStatus = new SelectList(Enum.JournalStatus.GetValues(typeof(Enum.JournalStatus)).OfType<Enum.JournalStatus>()
              .Select(x =>
                    new SelectListItem
                    {
                        Text = Enum.JournalStatus.GetName(typeof(Enum.JournalStatus), x),
                        Value = (Convert.ToInt32(x)).ToString()
                    }), "Value", "Text");

            return PartialView("_CreateEditJournalForm", createJournal);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateJournal(int projectId, JournalDetailDto journal)
        {
            if (!ModelState.IsValid) return new JsonResult(new { Success = false, Error = "Error while creating Journal!" });
            if(journal.Status == (int) Enquiry.Web.Enum.JournalStatus.ToApply)
            {
                journal.DateOfSubmission = null;
            }
            if (journal.Status == (int)Enquiry.Web.Enum.JournalStatus.MailAccount)
            {
                journal.DateOfSubmission = null;
            }
            
            if (journal.PublicationId == 0)
            {
                var result = await _project.CreateJournalAsync(journal, projectId);
                return Json(new { result.Publication });
            }
            else
            {
                var result = await _project.UpdateJournalAsync(journal);
                return Json(new { result.Publication });
            }

        }

        [HttpGet]
        public async Task<IActionResult> UpdateJournalForm(int publicationid)
        {

            var result = await _publication.GetJournalAsync(publicationid);

            ViewBag.JournalStatus = new SelectList(Enum.JournalStatus.GetValues(typeof(Enum.JournalStatus)).OfType<Enum.JournalStatus>()
              .Select(x =>
                    new SelectListItem
                    {
                        Text = Enum.JournalStatus.GetName(typeof(Enum.JournalStatus), x),
                        Value = (Convert.ToInt32(x)).ToString()
                    }), "Value", "Text");

            if(result.Data.DateOfSubmission is null)
                result.Data.DateOfSubmission = DateTime.Now;

            return PartialView("_CreateEditJournalForm", result.Data);
        }
    }
}
