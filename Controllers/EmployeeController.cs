using Enquiry.Web.Dtos;
using Enquiry.Web.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Enquiry.Web.Controllers;

[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class EmployeeController : Controller
{
    readonly IEmployee _employee;
    readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployee employee, ILogger<EmployeeController> logger)
    {
        _employee = employee;
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var result = await _employee.GetEmployeeAsync();
        return PartialView("_GetEmployees", result.Data);
    }

    public async Task<IActionResult> CreateEmployeeForm()
    {
        var roles = await _employee.GetRolesAsync();
        List<SelectListItem> roleList = new List<SelectListItem>();
        foreach (var role in roles.Data)
        {
            roleList.Add(new SelectListItem
            {
                Text = role.RoleName,
                Value = role.RoleId.ToString()

            });
        }
        var Dep = await _employee.GetDepartmentAsync();
        List<SelectListItem> deptList = new List<SelectListItem>();
        foreach (var role in Dep.Data)
        {
            deptList.Add(new SelectListItem
            {
                Text = role.DeptName,
                Value = role.DeptId.ToString()

            });
        }
        ViewBag.RoleList = roleList;
        ViewBag.DeptList = deptList;
        return PartialView("_CreateEmployeeForm");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeDto dto)
    {
        if (!ModelState.IsValid) return new JsonResult(new { Success = false, Error = "Error while creating employee!" });
        var data = await _employee.CreateEmployeeAsync(dto);
        return Json(data);
    }
}
