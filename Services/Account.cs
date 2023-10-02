using Enquiry.Web.Models;
using Enquiry.Web.Services.Interface;

namespace Enquiry.Web.Services
{
    public class Account : IAccount
    {
        readonly ApplicationDbContext _context;

        public Account(ApplicationDbContext context) { _context = context; }
    }
}
