using Enquiry.Web.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enquiry.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class VisitorController : Controller
    {
        readonly ILogger<VisitorController> _logger;
        readonly IVisitor _visitor;
        public VisitorController(IVisitor visitor, ILogger<VisitorController> logger)
        {
            _visitor = visitor;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> VistorsAsync()
        {
            var data = await _visitor.GetVisitorAsync();
            return PartialView("_Visitors", data.Data);
        }
    }
}
