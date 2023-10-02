using Enquiry.Web.Dtos;
using Enquiry.Web.Extensions;
using Enquiry.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;

namespace Enquiry.Web.Controllers
{
    public class BaseController : Controller
    {
        readonly ApplicationDbContext _context;
        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected ProfileConstants GetUserFromCookie()
        {
            var token = HttpContext.Request.Cookies[".AspNetCore.Cookies"];

            var opt = HttpContext.RequestServices
        .GetRequiredService<IOptionsMonitor<CookieAuthenticationOptions>>()
        .Get(CookieAuthenticationDefaults.AuthenticationScheme); //or use .Get("Cookies")

            var cookie = opt.CookieManager.GetRequestCookie(HttpContext, ".AspNetCore.Cookies");

            var handler = opt.TicketDataFormat.Unprotect(cookie);

            var val = handler.Principal;
            var constants = new ProfileConstants()
            {
                UserName = val.Claims.First(claim => claim.Type == "name").Value,
                Roles = val.Claims.First(claim => claim.Type == "role").Value,
                Dept = val.Claims.First(claim => claim.Type == "Dept").Value,
                EmailId = val.Claims.First(claim => claim.Type == "email").Value,
                EmployeeName = val.Claims.First(claim => claim.Type == "EmployeeName").Value,
                EmpId = int.Parse(val.Claims.First(claim => claim.Type == "EmpId").Value)
            };
            HttpContext.Session.SetInt32("CurrentUserId", constants.EmpId);
            HttpContext.Session.SetString("CurrentUserName", constants.UserName);
            HttpContext.Session.SetString("EmployeeName", constants.EmployeeName);
            HttpContext.Session.SetString("Role", constants.Roles);
            HttpContext.Session.SetString("Dept", constants.Dept);
            /*var host = Dns.GetHostEntry(Dns.GetHostName());*/
            string myIP = Response.HttpContext.Connection.RemoteIpAddress.ToString();
            if (myIP == "::1")
            {
                myIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            }
            var visitor = new Visitors()
            {
                EmployeeId = constants.EmpId,
                NetworkId = myIP,
                IsClient = false,
                LoginTime = DatetimeExtension.IndiaStandardTimeNow()
            };
            _context.Add(visitor);
            _context.SaveChanges();
            return constants;
        }
    }
}
