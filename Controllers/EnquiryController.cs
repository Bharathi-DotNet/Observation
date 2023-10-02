using Enquiry.Web.Dtos;
using Enquiry.Web.Models;
using Enquiry.Web.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Enquiry.Web.Controllers;

[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class EnquiryController : BaseController
{
    readonly IClient _client;
    readonly IEmployee _employee;
    readonly ILogger<EnquiryController> _logger;
    private readonly ISession _session;
    public EnquiryController(IClient client, ILogger<EnquiryController> logger, IEmployee employee, 
        IHttpContextAccessor httpContextAccessor, ApplicationDbContext context) : base(context)
    {
        _client = client;
        _logger = logger;
        _employee = employee;
        _session = httpContextAccessor.HttpContext.Session;

        if (_session.GetString("ClientsFiler") is null)
        //if (_session.GetString("Dept") == Enum.Department.BDA.ToString())
        {
            _session.SetString("ClientsFiler", "A");
        }
        if (_session.GetString("Dept") == Enum.Department.Tech.ToString())
        {
            _session.SetString("ClientsFiler", "E");
        }

    }
    public IActionResult Index()
    {
        this.GetUserFromCookie();
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetClients(char chkValue)
    {
        if (chkValue == '\0')
        {
            chkValue = _session.GetString("ClientsFiler").ToCharArray()[0];
        }
        var data = await _client.GetClientsAsync(chkValue);
        _session.SetString("ClientsFiler", chkValue.ToString());

        if (data.Succeeded) return PartialView("_GetClients", data.Data);
        _logger.LogError($"Error - {data.Error}");
        return RedirectToAction("Error", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> GetClientDetail(int enquiryId)
    {
        var bda = await _employee.GetBDAAsync();
        List<SelectListItem> bdaList = new List<SelectListItem>();
        foreach (var role in bda.Data)
        {
            bdaList.Add(new SelectListItem
            {
                Text = role.EmployeeName,
                Value = role.EmpId.ToString()

            });
        }
        var tec = await _employee.GetTechExpertAsync();
        List<SelectListItem> techList = new List<SelectListItem>();
        foreach (var role in tec.Data)
        {
            techList.Add(new SelectListItem
            {
                Text = role.EmployeeName,
                Value = role.EmpId.ToString()

            });
        }
        ViewBag.TechExpert = techList;
        ViewBag.BDAList = bdaList;
        var result = await _client.GetClientDetailAsync(enquiryId);
        return PartialView("_GetClientDetail", result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetClientActitvity(int enquiryId, bool isComment)
    {
        var activity = await _client.GetClientActivityAsync(enquiryId, isComment);
        return PartialView("_GetClientActivity", activity.Data);
    }

    [HttpGet]
    public async Task<IActionResult> CreateClientForm()
    {
        var tec = await _employee.GetTechExpertAsync();

        ViewBag.techExpert = tec.Data.Select(x => new SelectListItem() { Text = x.EmployeeName, Value = x.EmpId.ToString() }).ToList();
        var createClient = new CreateClientDto();
        return PartialView("_CreateClientForm", createClient);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateClient(CreateClientDto dto)
    {
        if (!ModelState.IsValid) return new JsonResult(new { Success = false, Error = "Error while creating client!" });
        var data = await _client.CreateClientAsync(dto);
        return Json(new { data });
    }

    [HttpGet]
    public async Task<IActionResult> EditClientForm(int enquiryId)
    {
        var result = await _client.EditClientFormAsync(enquiryId);
        return PartialView("_EditClientForm", result.data);
    }

    [HttpPost]
    public async Task<IActionResult> EditClient(EditClientDto dto)
    {
        var result = await _client.EditClientAsync(dto);
        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteClient(int enquiryId)
    {
        var data = await _client.DeleteClientAsync(enquiryId);
        return Json(data);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateBDAForClient(int bdaId, int enquiryId)
    {
        var data = await _client.UpdateBDAForClientAsync(bdaId, enquiryId);
        return Json(data);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTechExpertForClient(int techId, int enquiryId)
    {
        var data = await _client.UpdateTechExpertForClientAsync(techId, enquiryId);
        return Json(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetClientActivity(int enquiryId, bool isComment)
    {
        var result = await _client.GetClientActivityAsync(enquiryId, isComment);
        return PartialView("_GetClientActivity", result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> UpdatePaymentRemarks(int enquiryId, string remarks, bool isRegsitered)
    {
        var data = await _client.UpdatePaymentRemarksAsync(enquiryId, remarks, isRegsitered);
        return Json(data);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateComments(int enquiryId, string remarks)
    {
        await _client.AddEnquiryActivity(enquiryId, (int)_session.GetInt32("CurrentUserId"), remarks, true);
        return Json(Task.CompletedTask);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateAppoinmentDate(DateTime appoinmentDate, int enquiryId)
    {
        var data = await _client.UpdateAppoinmentDateAsync(appoinmentDate, enquiryId);
        return Json(data);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateAsTechAppoinment(bool ChkValue, int enquiryId)
    {
        var data = await _client.UpdateAsTechAppoinmentAsync(ChkValue, enquiryId);
        return Json(data);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateBackToEnquiry(int enquiryId)
    {
        var data = await _client.UpdateBackToEnquiryAsync(enquiryId);
        return Json(data);
    }
}
