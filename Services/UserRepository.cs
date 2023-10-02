using Enquiry.Web.Dtos;
using Enquiry.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Enquiry.Web.Services
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string username)
        {
            var val = await _context.Employees.Include(x => x.Department)
                .Include(x => x.Roles).SingleOrDefaultAsync(u => u.IdentityNo.Equals(username));
            if (val == null) return null;
            return new User()
            {
                Username = val.IdentityNo,
                Password = val.Password,
                EmpId = val.EmpId.ToString(),
                Email = val.EmailId,
                EmployeeName = val.EmployeeName,
                Roles = new string[] { val.Roles.RoleName },
                Dept = val.Department.DeptName
            };
        }
    }
}
